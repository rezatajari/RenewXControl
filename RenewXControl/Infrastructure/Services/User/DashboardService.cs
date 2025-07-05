using Microsoft.AspNetCore.Identity;
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

    public async Task<Profile> GetDashboardDataAsync(string userId)
    {
        var user = await _userManager.FindByIdAsync(userId);
        var role = await _userManager.GetRolesAsync(user);
        var totalAssets = await _assetRepository.CountByUserIdAsync(userId);

        return new Profile(totalAssets, user.UserName , role);
    }
}