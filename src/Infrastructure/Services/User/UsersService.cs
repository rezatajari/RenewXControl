using Application.Common;
using Application.DTOs.User.Profile;
using Application.Interfaces.Asset;
using Application.Interfaces.File;
using Application.Interfaces.User;
using Infrastructure.Persistence;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Services.User;

public class UsersService:IUsersService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IAssetRepository _assetRepository;
    private readonly IWebHostEnvironment _env;


    public UsersService(
        UserManager<ApplicationUser> userManager,
        IAssetRepository assetRepository, 
        IWebHostEnvironment env)
    {
        _userManager=userManager;
        _assetRepository=assetRepository;
        _env = env;
    }

    public async Task<GeneralResponse<Profile>> GetProfile(Guid userId)
    {
        var userValidation = ValidateUserId(userId);
        if (!userValidation.IsSuccess)
            return GeneralResponse<Profile>.Failure(message:userValidation.Message,errors:userValidation.Errors);


        var user = await _userManager.FindByIdAsync(userId.ToString());
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

        var role = await _userManager.GetRolesAsync(user);
        var totalAssets = await _assetRepository.GetTotalAssets(userId);
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

        var user = await _userManager.FindByIdAsync(userId.ToString());
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

       var result= await _userManager.UpdateAsync(user);
        
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
        var user = await _userManager.FindByIdAsync(userId.ToString());
        var result = await _userManager.ChangePasswordAsync(
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
            var uploadFolder = Path.Combine(_env.WebRootPath, "profile-images");

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
}