using Microsoft.AspNetCore.Identity;
using RenewXControl.Api.Utility;
using RenewXControl.Application.Asset.Interfaces;
using RenewXControl.Application.DTOs.User;
using RenewXControl.Application.User;

namespace RenewXControl.Infrastructure.Services.User;

public class DashboardService:IDashboardService
{
    private readonly UserManager<Domain.User> _userManager;
    private readonly IAssetRepository _assetRepository;

    public DashboardService(UserManager<Domain.User> userManager,IAssetRepository assetRepository)
    {
        _userManager=userManager;
        _assetRepository=assetRepository;
    }

    public async Task<GeneralResponse<Profile>> GetDashboardDataAsync(string userId)
    {
        if (string.IsNullOrWhiteSpace(userId))
            return  GeneralResponse<Profile>.Failure(
                message: "Credential failed",
                errors:
                [
                    new ErrorResponse
                    {
                        Name = "User detection",
                        Message = $"Your user is not found"
                    }
                ]);


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
        var totalAssets = await _assetRepository.CountByUserIdAsync(userId);
        var profile = new Profile(totalAssets, user.UserName, role);

        return GeneralResponse<Profile>.Success(
            data: profile,
            message: "Get profile information successfully"
        );
    }
}