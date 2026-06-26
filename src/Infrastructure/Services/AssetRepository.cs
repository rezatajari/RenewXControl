using Application.Interfaces.Asset;
using Domain.Entities.Assets;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services;

public class AssetRepository(RxcDbContext context) : IAssetRepository
{
    public async Task<int> GetTotalAssets(Guid userId)
    {

        return await (from asset in context.Assets
            join site in context.Sites on asset.SiteId equals site.Id
            where site.UserId == userId
            select asset).CountAsync();
    }

    public async Task AddAssetAsync(Domain.Entities.Assets.Asset asset)
    {
        await context.Assets.AddAsync(asset);
    }

    public async Task<SolarPanel> GetSolarByUserId(Guid userId)
    {
        return await context.Assets.Include(s => s.Site)
            .OfType<SolarPanel>()
            .Where(u => u.Site != null && u.Site.UserId == userId)
            .FirstOrDefaultAsync();
    }

    public async Task<WindTurbine> GetTurbineByUserId(Guid userId)
    {
        return await context.Assets.Include(s => s.Site)
            .OfType<WindTurbine>()
            .Where(u => u.Site != null && u.Site.UserId == userId)
            .FirstOrDefaultAsync();
    }

    public async Task<Battery> GetBatteryByUserId(Guid userId)
    {
        return await context.Assets.Include(s => s.Site)
            .OfType<Battery>()
            .Where(u => u.Site != null && u.Site.UserId == userId)
            .FirstOrDefaultAsync();
    }
}