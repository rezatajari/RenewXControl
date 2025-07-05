using RenewXControl.Api.Utility;
using RenewXControl.Application.Asset.Interfaces;
using RenewXControl.Application.DTOs.AddAsset;
using RenewXControl.Application.DTOs.AssetMonitoring;
using RenewXControl.Domain.Assets;
using RenewXControl.Domain.Interfaces.Assets;
using RenewXControl.Infrastructure.Persistence;
using RenewXControl.Infrastructure.Services.Asset;
using Battery = RenewXControl.Domain.Assets.Battery;
using Console = System.Console;

namespace RenewXControl.Application.Asset.Implementation
{
    public class AssetService : IAssetService
    {
        private readonly IAssetRepository _assetRepository;
        private readonly RxcDbContext _context;

        public AssetService(
            IAssetRepository assetRepository, 
            RxcDbContext context)
        {
            _assetRepository = assetRepository;
            _context = context;
        }

        public async Task<GeneralResponse<Guid>> AddSiteAsync(AddSite addSite, string userId)
        {
            var site = Site.Create(addSite,userId);
            await _assetRepository.AddSite(site);

            await _context.SaveChangesAsync();

            return GeneralResponse<Guid>.Success(data: site.Id, "Site added successfully");
        }

        public async Task<GeneralResponse<Guid>> AddBatteryAsync(AddBattery addBattery, Guid siteId)
        {

            var battery = Battery.Create(addBattery,siteId);
            await _assetRepository.AddAssetAsync(battery);

            await _context.SaveChangesAsync();

            return GeneralResponse<Guid>.Success(data: battery.Id, "Battery added successfully");
        }

        public async Task<GeneralResponse<Guid>> AddSolarAsync(AddSolar addSolar, Guid siteId)
        {
            var solar = SolarPanel.Create(addSolar, siteId);
            await _assetRepository.AddAssetAsync(solar);

            await _context.SaveChangesAsync();

            return GeneralResponse<Guid>.Success(solar.Id, "Solar added successfully");
        }

        public async Task<GeneralResponse<Guid>> AddTurbineAsync(AddTurbine addTurbine, Guid siteId)
        {
            var turbine = WindTurbine.Create(addTurbine, siteId);
            await _assetRepository.AddAssetAsync(turbine);

            await _context.SaveChangesAsync();

            return GeneralResponse<Guid>.Success(turbine.Id, "Turbine added successfully");
        }
    }
}
