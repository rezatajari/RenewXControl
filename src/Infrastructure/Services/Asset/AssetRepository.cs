using Application.Interfaces.Asset;
using Domain.Entities.Assets;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services.Asset;

public class AssetRepository : IAssetRepository
{
    private readonly RxcDbContext _context;
    public AssetRepository(RxcDbContext context)
    {
        _context = context;
    }

    public async Task<int> GetTotalAssets(Guid userId)
    {

        return await (from asset in _context.Assets
            join site in _context.Sites on asset.SiteId equals site.Id
            where site.UserId == userId
            select asset).CountAsync();
    }

    public async Task AddAssetAsync(Domain.Entities.Assets.Asset asset)
    {
        await _context.Assets.AddAsync(asset);
    }

    public async Task<SolarPanel> GetSolarByUserId(Guid userId)
    {
        return await _context.Assets.Include(s => s.Site)
            .OfType<SolarPanel>()
            .Where(u => u.Site != null && u.Site.UserId == userId)
            .FirstOrDefaultAsync();
    }

    public async Task<WindTurbine> GetTurbineByUserId(Guid userId)
    {
        return await _context.Assets.Include(s => s.Site)
            .OfType<WindTurbine>()
            .Where(u => u.Site != null && u.Site.UserId == userId)
            .FirstOrDefaultAsync();
    }

    public async Task<Battery> GetBatteryByUserId(Guid userId)
    {
        return await _context.Assets.Include(s => s.Site)
            .OfType<Battery>()
            .Where(u => u.Site != null && u.Site.UserId == userId)
            .FirstOrDefaultAsync();
    }
}