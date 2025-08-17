using Application.Common;
using Application.DTOs;
using Application.DTOs.User.Profile;
using Application.Interfaces.Asset;
using Application.Interfaces.User;
using Domain.Entities.Assets;
using Infrastructure.Persistence;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services.User;

public class UsersService(
    UserManager<ApplicationUser> userManager,
    IAssetRepository assetRepository,
    IWebHostEnvironment env,
    RxcDbContext db)
    : IUsersService
{

    public async Task<GeneralResponse<Profile>> GetProfile(Guid userId)
    {
        var userValidation = ValidateUserId(userId);
        if (!userValidation.IsSuccess)
            return GeneralResponse<Profile>.Failure(message:userValidation.Message,errors:userValidation.Errors);


        var user = await userManager.FindByIdAsync(userId.ToString());
        if(user is null)
            return GeneralResponse<Profile>.Failure(
                message:"User found",
                errors: [
                    new ErrorResponse
                    {
                        Name = "User find",
                        Message = "User is not found"
                    }
                    ]);

        var role = await userManager.GetRolesAsync(user);
        var totalAssets = await assetRepository.GetTotalAssets(userId);
        var profile = new Profile(totalAssets, user.UserName, role,user.ProfileImage);

        return GeneralResponse<Profile>.Success(
            data: profile,
            message: "Get profile information successfully"
        );
    }

    public async Task<GeneralResponse<bool>> EditProfile(EditProfile editProfile,Guid userId)
    {
        var userValidation = ValidateUserId(userId);
        if (!userValidation.IsSuccess)
            return GeneralResponse<bool>.Failure(message: userValidation.Message, errors: userValidation.Errors);

        var user = await userManager.FindByIdAsync(userId.ToString());
        if (user is null)
            return GeneralResponse<bool>.Failure(
                message: "User found",
                errors: [
                    new ErrorResponse
                    {
                        Name = "User find",
                        Message = "User is not found"
                    }
                ]);

        user.UserName= editProfile.UserName;
        user.ProfileImage = editProfile.ProfileImage;

       var result= await userManager.UpdateAsync(user);
        
       if (result.Succeeded)
        return GeneralResponse<bool>.Success(
            data:true,
            message:"Profile updated successfully."
            );

       return GeneralResponse<bool>.Failure(
           message: "Profile updated failed",
           errors:null);
    }

    public GeneralResponse<bool> ValidateUserId(Guid userId)
    {
        if (userId == Guid.Empty)
        {
            return GeneralResponse<bool>.Failure(
                message: "User detection failed",
                errors: [
                    new ErrorResponse { Name = "User Credential", Message = "User ID is required." }
                ]);
        }

        return GeneralResponse<bool>.Success(true);
    }

    public async Task<GeneralResponse<bool>> ChangePasswordAsync(ChangePassword changePassword, Guid userId)
    {
        var user = await userManager.FindByIdAsync(userId.ToString());
        var result = await userManager.ChangePasswordAsync(
            user,
            changePassword.CurrentPassword,
            changePassword.NewPassword);

        if (result.Succeeded)
            return GeneralResponse<bool>.Success(
                data: true,
                message: "Your update password successful"
            );


        var errors = result.Errors
            .Select(error => new ErrorResponse(
                name: error.Code.ToString(),
                message: error.Description))
            .ToList();

        return GeneralResponse<bool>.Failure(
            message: "Password update failed.",
            errors: errors);
    }

    public async Task<GeneralResponse<string>> SaveProfileImageAsync(IUploadedFile file)
    {
        if (file == null || file.Length == 0)
            return GeneralResponse<string>.Failure("File is missing or empty.");

        var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif", ".webp" };
        var fileExtension = Path.GetExtension(file.FileName).ToLower();

        if (!allowedExtensions.Contains(fileExtension))
            return GeneralResponse<string>.Failure("Invalid file type. Only images are allowed.");

        if (file.Length > 5_000_000) // 5MB limit
            return GeneralResponse<string>.Failure("File size exceeds 5MB limit.");

        try
        {
            // Use wwwroot/profile-images for better organization
            var uploadFolder = Path.Combine(env.WebRootPath, "profile-images");

            if (!Directory.Exists(uploadFolder))
                Directory.CreateDirectory(uploadFolder);

            var uniqueFileName = $"{Guid.NewGuid()}{fileExtension}";
            var filePath = Path.Combine(uploadFolder, uniqueFileName);

            await using var stream = new FileStream(filePath, FileMode.Create);
            await file.OpenReadStream().CopyToAsync(stream);

            // Return path relative to wwwroot
            return GeneralResponse<string>.Success($"profile-images/{uniqueFileName}");
        }
        catch (Exception ex)
        {
            return GeneralResponse<string>.Failure("File upload failed. Please try again.");
        }
    }

    public async Task<List<UserMonitoringInfo>> GetAllUsersWithSitesAndAssetsAsync()
    {
        var monitoringData = await db.Users
            .Include(u => u.Sites)
            .ThenInclude(s => s.Assets)
            .ToListAsync();

        var result = monitoringData
            .SelectMany(u => u.Sites, (user, site) =>
            {
                var solarPanel = site.Assets
                    .OfType<SolarPanel>()
                    .Select(sp => SolarPanel.Create(
                                sp.Irradiance,
                                sp.ActivePower,
                                sp.SetPoint,
                                site.Id))
                    .FirstOrDefault();

                var windTurbine = site.Assets
                    .OfType<WindTurbine>()
                    .Select(wt => WindTurbine.Create(
                                wt.WindSpeed,
                                wt.ActivePower,
                                wt.SetPoint,
                                site.Id))
                    .FirstOrDefault();

                var battery = site.Assets
                    .OfType<Battery>()
                    .Select(b => Battery.Create(
                        b.Capacity,
                        b.StateCharge,
                        b.SetPoint,
                        b.FrequentlyDisCharge,
                        site.Id))
                    .FirstOrDefault();

                return new UserMonitoringInfo
                {
                    Username = user.UserName,
                    SiteName = site.Name,
                    SiteLocation = site.Location,
                    SolarPanel = solarPanel,
                    WindTurbine = windTurbine,
                    Battery = battery
                };
            })
            .ToList();


        return result;
    }
}