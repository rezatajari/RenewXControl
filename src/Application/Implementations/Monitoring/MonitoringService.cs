using Application.Common;
using Application.Interfaces.Asset;
using Application.Interfaces.Monitoring;
using Application.Interfaces.User;
using Domain.Entities.Assets;

namespace Application.Implementations.Monitoring;

public class MonitoringService(
    IAssetService assetService,
    IAssetControlFactory assetControlFactory,
    IMonitoringRegistry monitoringRegistry,
    IUserValidator userValidator)
    : IMonitoringService
{
    public async Task<GeneralResponse<bool>> RegisterMonitoringSession(Guid userId)
    {
        var userValidation = userValidator.ValidateUserId(userId);
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

   private async Task<GeneralResponse<(SolarPanel, WindTurbine, Battery)>> LoadUserAssetsAsync(
        Guid userId)
    {
        var solarResult = await assetService.GetSolarByUserIdAsync(userId);
        if (!solarResult.IsSuccess)
            return GeneralResponse<(SolarPanel, WindTurbine, Battery)>.Failure(solarResult.Message,
                solarResult.Errors);

        var turbineResult = await assetService.GetTurbineByUserIdAsync(userId);
        if (!turbineResult.IsSuccess)
            return GeneralResponse<(SolarPanel, WindTurbine, Battery)>.Failure(turbineResult.Message,
                turbineResult.Errors);

        var batteryResult = await assetService.GetBatteryByUserIdAsync(userId);
        if (!batteryResult.IsSuccess)
            return GeneralResponse<(SolarPanel, WindTurbine, Battery)>.Failure(batteryResult.Message,
                batteryResult.Errors);

        return GeneralResponse<(SolarPanel, WindTurbine, Battery)>.Success((solarResult.Data,
            turbineResult.Data, batteryResult.Data));
    }


    private void RegisterRuntimeSession(Guid userId,
        (SolarPanel solar, WindTurbine turbine, Battery battery) assets)
    {
        var (solar, turbine, battery) = assets;

        var solarControl = assetControlFactory.CreateSolarControl(solar);
        var turbineControl = assetControlFactory.CreateTurbineControl(turbine);
        var batteryControl = assetControlFactory.CreateBatteryControl(battery);

        var operation = assetControlFactory.CreateAssetOperations(solarControl, turbineControl, batteryControl);

        monitoringRegistry.RegisterUser(userId, solarControl, turbineControl, batteryControl, operation);
    }

}