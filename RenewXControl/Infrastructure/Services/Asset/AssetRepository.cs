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

        public async Task<int> CountByUserIdAsync(string userId)
        {
            return await _context.Assets
                            .Where(a => _context.Sites.Any(s => s.Id == a.SiteId && s.UserId == userId))
                            .CountAsync();
        }

        public async Task AddAssetAsync(Domain.Assets.Asset asset)
        {
            await _context.Assets.AddAsync(asset);
        }

       public async Task<Site> GetSiteById(Guid siteId)
        {
           return await _context.Sites.FirstOrDefaultAsync(s => s.Id == siteId);
        }

        public async Task<SolarPanel> GetSolarById(Guid solarId)
        {
            return await _context.Assets
                .OfType<SolarPanel>()
                .FirstOrDefaultAsync(a => a.Id == solarId);
        }

        public async Task<WindTurbine> GetTurbineById(Guid turbineId)
        {
            return await _context.Assets
                .OfType<WindTurbine>()
                .FirstOrDefaultAsync(a => a.Id == turbineId);
        }

        public async Task<Battery> GetBatteryById(Guid batteryId)
        {
            return await _context.Assets
            .OfType<Battery>()
                .FirstOrDefaultAsync(a => a.Id == batteryId);
        }

        public async Task<SolarPanel> GetSolarByUserId(string userId)
        {
           return  await _context.Assets.Include(s => s.Site)
                .OfType<SolarPanel>()
                .Where(u => u.Site != null && u.Site.UserId == userId)
                .FirstOrDefaultAsync();
        }

        public async Task<WindTurbine> GetTurbineByUserId(string userId)
        {
            return await _context.Assets.Include(s => s.Site)
                .OfType<WindTurbine>()
                .Where(u =>u.Site!=null&& u.Site.UserId == userId)
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
