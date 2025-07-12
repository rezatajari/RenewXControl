using RenewXControl.Api.Utility;
using RenewXControl.Application.Asset.Interfaces;
using RenewXControl.Application.DTOs.AddAsset;
using RenewXControl.Application.DTOs.AssetMonitoring;
using RenewXControl.Domain;
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
        private readonly ISiteService _siteService;
        private readonly IAssetControlFactory _assetControlFactory;
        private readonly IMonitoringRegistry _monitoringRegistry;
        private readonly RxcDbContext _context;

        public AssetService(
            IAssetRepository assetRepository,
            ISiteService siteService,
            IAssetControlFactory assetControlFactory,
            RxcDbContext context,
            IMonitoringRegistry monitoringRegistry)
        {
            _assetRepository = assetRepository;
            _siteService = siteService;
            _assetControlFactory = assetControlFactory;
            _context = context;
            _monitoringRegistry = monitoringRegistry;
        }


        public async Task<GeneralResponse<Guid>> AddBatteryAsync(AddBattery addBattery,string userId)
        {
            if (string.IsNullOrWhiteSpace(userId))
            {
                return GeneralResponse<Guid>.Failure(
                    message: "User detection failed",
                    errors: [
                        new ErrorResponse
                        {
                            Name = "User credential",
                            Message = "User ID is required"
                        }
                    ]);
            }

            var siteIdResponse = await _siteService.GetSiteId(userId);
            var siteId = siteIdResponse.Data;

            var battery = Battery.Create(addBattery,siteId);
            await _assetRepository.AddAssetAsync(battery);

            await _context.SaveChangesAsync();

            return GeneralResponse<Guid>.Success(
                data: battery.Id,
                message:"Battery added successfully");
        }

        public async Task<GeneralResponse<Guid>> AddSolarAsync(AddSolar addSolar, string userId)
        {
            if (string.IsNullOrWhiteSpace(userId))
            {
                return GeneralResponse<Guid>.Failure(
                    message: "User detection failed",
                    errors: [
                        new ErrorResponse
                        {
                            Name = "User credential",
                            Message = "User ID is required"
                        }
                    ]);
            }

            var siteIdResponse = await _siteService.GetSiteId(userId);
            var siteId = siteIdResponse.Data;

            var solar = SolarPanel.Create(addSolar, siteId);
            await _assetRepository.AddAssetAsync(solar);

            await _context.SaveChangesAsync();

            return GeneralResponse<Guid>.Success(
                solar.Id, 
                message:"Solar added successfully");
        }

        public async Task<GeneralResponse<Guid>> AddTurbineAsync(AddTurbine addTurbine, string userId)
        {
            if (string.IsNullOrWhiteSpace(userId))
            {
                return GeneralResponse<Guid>.Failure(
                    message: "User detection failed",
                    errors: [
                        new ErrorResponse
                        {
                            Name = "User credential",
                            Message = "User ID is required"
                        }
                    ]);
            }

            var siteIdResponse = await _siteService.GetSiteId(userId);
            var siteId = siteIdResponse.Data;

            var turbine = WindTurbine.Create(addTurbine, siteId);
            await _assetRepository.AddAssetAsync(turbine);

            await _context.SaveChangesAsync();

            return GeneralResponse<Guid>.Success(
                turbine.Id,
                message:"Turbine added successfully");
        }

        public async Task MonitoringData(string userId)
        {
            // Get user assets from DB
            var solar = await _assetRepository.GetSolarByUserId(userId);
            var turbine = await _assetRepository.GetTurbineByUserId(userId);
            var battery = await _assetRepository.GetBatteryByUserId(userId);

            // Create runtime controls
            var solarControl =  _assetControlFactory.CreateSolarControlAsync(solar);
            var turbineControl=  _assetControlFactory.CreateTurbineControlAsync(turbine);
            var batteryControl=  _assetControlFactory.CreateBatteryControlAsync(battery);

            var assetControl =
                _assetControlFactory.CreateAssetRuntimeOperationAsync(solarControl, turbineControl, batteryControl);

            _monitoringRegistry.RegisterUser(userId,solarControl,turbineControl,batteryControl,assetControl);
        }
    }
}
