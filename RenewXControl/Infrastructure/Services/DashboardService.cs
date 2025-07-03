using Microsoft.AspNetCore.Identity;
using RenewXControl.Application.DTOs;
using RenewXControl.Application.Interfaces;
using RenewXControl.Domain.Users;

namespace RenewXControl.Infrastructure.Services;

public class DashboardService:IDashboardService
{
    private readonly UserManager<User> _userManager;
    private readonly IAssetRepository _assetRepository;

    public DashboardService(UserManager<User> userManager,IAssetRepository assetRepository)
    {
        _userManager=userManager;
        _assetRepository=assetRepository;
    }

    public async Task<Dashboard> GetDashboardDataAsync(string userId)
    {
        var user = await _userManager.FindByIdAsync(userId);
        var role = await _userManager.GetRolesAsync(user);
        var totalAssets = await _assetRepository.CountByUserIdAsync(userId);

        return new Dashboard(totalAssets, user.UserName , role);
    }
}