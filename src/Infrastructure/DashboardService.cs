using Application.Common;
using Application.DTOs.User.Profile;
using Application.Interfaces.Asset;
using Application.Interfaces.User;
using Infrastructure.Persistence;
using Microsoft.AspNetCore.Identity;

namespace Application.Implementations;

public class DashboardService:IDashboardService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IAssetRepository _assetRepository;
    private readonly IUserValidator _userValidator;

    public DashboardService(UserManager<ApplicationUser> userManager,IAssetRepository assetRepository,IUserValidator userValidator)
    {
        _userManager=userManager;
        _assetRepository=assetRepository;
        _userValidator=userValidator;
    }

    public async Task<GeneralResponse<Profile>> GetProfile(Guid userId)
    {
        var userValidation = _userValidator.ValidateUserId(userId);
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
        var userValidation = _userValidator.ValidateUserId(userId);
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
}