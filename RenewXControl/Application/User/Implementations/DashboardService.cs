using Microsoft.AspNetCore.Identity;
using RenewXControl.Api.Utility;
using RenewXControl.Application.Asset.Interfaces;
using RenewXControl.Application.Asset.Interfaces.Asset;
using RenewXControl.Application.DTOs.User;
using RenewXControl.Application.User.Interfaces;

namespace RenewXControl.Application.User.Implementations;

public class DashboardService:IDashboardService
{
    private readonly UserManager<Domain.User.User> _userManager;
    private readonly IAssetRepository _assetRepository;
    private readonly IUserValidator _userValidator;

    public DashboardService(UserManager<Domain.User.User> userManager,IAssetRepository assetRepository,IUserValidator userValidator)
    {
        _userManager=userManager;
        _assetRepository=assetRepository;
        _userValidator=userValidator;
    }

    public async Task<GeneralResponse<Profile>> GetProfile(string userId)
    {
        var userValidation = _userValidator.ValidateUserId(userId);
        if (!userValidation.IsSuccess)
            return GeneralResponse<Profile>.Failure(message:userValidation.Message,errors:userValidation.Errors);


        var user = await _userManager.FindByIdAsync(userId);
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
        var profile = new Profile(totalAssets, user.UserName, role);

        return GeneralResponse<Profile>.Success(
            data: profile,
            message: "Get profile information successfully"
        );
    }
}