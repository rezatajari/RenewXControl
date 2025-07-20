using RenewXControl.Api.Utility;
using RenewXControl.Application.Asset.Interfaces;
using RenewXControl.Application.Asset.Interfaces.Asset;
using RenewXControl.Application.Asset.Interfaces.Monitoring;
using RenewXControl.Application.DTOs.AssetMonitoring;
using RenewXControl.Domain.Assets;
using Battery = RenewXControl.Application.DTOs.AssetMonitoring.BatteryDto;

namespace RenewXControl.Application.Asset.Implementation.Monitoring
{
    public class MonitoringService : IMonitoringService
    {
        private readonly IAssetService _assetService;
        private readonly IAssetControlFactory _assetControlFactory;
        private readonly IMonitoringRegistry _monitoringRegistry;
        private readonly IUserValidator _userValidator;

        public MonitoringService(
            IAssetService assetService,
            IAssetControlFactory assetControlFactory,
            IMonitoringRegistry monitoringRegistry,
            IUserValidator userValidator)
        {
            _assetService = assetService;
            _assetControlFactory = assetControlFactory;
            _monitoringRegistry = monitoringRegistry;
            _userValidator = userValidator;
        }

        public async Task<GeneralResponse<bool>> RegisterMonitoringSession(string userId)
        {
            var userValidation = _userValidator.ValidateUserId(userId);
            if (!userValidation.IsSuccess)
                return userValidation;

            // Get user assets from DB
            var response = await LoadUserAssetsAsync(userId);
            if (!response.IsSuccess)
                return GeneralResponse<bool>.Failure(message:response.Message,errors:response.Errors);

            // Create controls of assets in runtime
            RegisterRuntimeSession(userId, response.Data);

            return GeneralResponse<bool>.Success(
                data: true,
                message: "Monitoring asset is registration"
            );
        }

        private async Task<GeneralResponse<(SolarPanel, WindTurbine, Domain.Assets.Battery)>> LoadUserAssetsAsync(
            string userId)
        {
            var solarResult = await _assetService.GetSolarByUserIdAsync(userId);
            if (!solarResult.IsSuccess)
                return GeneralResponse<(SolarPanel, WindTurbine, Domain.Assets.Battery)>.Failure(solarResult.Message,
                    solarResult.Errors);

            var turbineResult = await _assetService.GetTurbineByUserIdAsync(userId);
            if (!turbineResult.IsSuccess)
                return GeneralResponse<(SolarPanel, WindTurbine, Domain.Assets.Battery)>.Failure(turbineResult.Message,
                    turbineResult.Errors);

            var batteryResult = await _assetService.GetBatteryByUserIdAsync(userId);
            if (!batteryResult.IsSuccess)
                return GeneralResponse<(SolarPanel, WindTurbine, Domain.Assets.Battery)>.Failure(batteryResult.Message,
                    batteryResult.Errors);

            return GeneralResponse<(SolarPanel, WindTurbine, Domain.Assets.Battery)>.Success((solarResult.Data,
                turbineResult.Data, batteryResult.Data));
        }


        private void RegisterRuntimeSession(string userId,
            (SolarPanel solar, WindTurbine turbine, Domain.Assets.Battery battery) assets)
        {
            var (solar, turbine, battery) = assets;

            var solarControl = _assetControlFactory.CreateSolarControl(solar);
            var turbineControl = _assetControlFactory.CreateTurbineControl(turbine);
            var batteryControl = _assetControlFactory.CreateBatteryControl(battery);

            var operation = _assetControlFactory.CreateAssetOperations(solarControl, turbineControl, batteryControl);

            _monitoringRegistry.RegisterUser(userId, solarControl, turbineControl, batteryControl, operation);
        }

    }
}
