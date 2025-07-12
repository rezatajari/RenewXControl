using Microsoft.EntityFrameworkCore;
using RenewXControl.Application.Asset.Interfaces;
using RenewXControl.Application.DTOs.AssetMonitoring;
using RenewXControl.Domain.Assets;
using RenewXControl.Infrastructure.Persistence;
using Battery = RenewXControl.Domain.Assets.Battery;

namespace RenewXControl.Infrastructure.Services.Asset
{
    public class AssetRepository : IAssetRepository
    {
        private readonly RxcDbContext _context;
        public AssetRepository(RxcDbContext context)
        {
            _context = context;
        }

        public async Task<int> GetTotalAssets(string userId)
        {

            return await (from asset in _context.Assets
                          join site in _context.Sites on asset.SiteId equals site.Id
                          where site.UserId == userId
                          select asset).CountAsync();
        }

        public async Task AddAssetAsync(Domain.Assets.Asset asset)
        {
            await _context.Assets.AddAsync(asset);
        }

      public async Task<SolarPanel> GetSolarByUserId(string userId)
        {
            return await _context.Assets.Include(s => s.Site)
                 .OfType<SolarPanel>()
                 .Where(u => u.Site != null && u.Site.UserId == userId)
                 .FirstOrDefaultAsync();
        }

        public async Task<WindTurbine> GetTurbineByUserId(string userId)
        {
            return await _context.Assets.Include(s => s.Site)
                .OfType<WindTurbine>()
                .Where(u => u.Site != null && u.Site.UserId == userId)
                .FirstOrDefaultAsync();
        }

        public async Task<Battery> GetBatteryByUserId(string userId)
        {
            return await _context.Assets.Include(s => s.Site)
                .OfType<Battery>()
                .Where(u => u.Site != null && u.Site.UserId == userId)
                .FirstOrDefaultAsync();
        }
    }
}
