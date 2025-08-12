using Application.Common;
using Application.DTOs;
using Application.Interfaces.Asset;
using Application.Interfaces.Monitoring;
using Application.Interfaces.User;

namespace Application.Implementations.Monitoring;

public class MonitoringService(
    IAssetService assetService,
    IAssetControlFactory assetControlFactory,
    IUsersService userService)
    : IMonitoringService
{
    public async Task<GeneralResponse<MonitoringAssetControl>> MonitoringAssetControl(Guid userId)
    {
        var userValidation = userService.ValidateUserId(userId);
        if (!userValidation.IsSuccess)
            return GeneralResponse<MonitoringAssetControl>.Failure();

        var solarResult = await assetService.GetSolarByUserIdAsync(userId);
        if (!solarResult.IsSuccess)
            return GeneralResponse<MonitoringAssetControl>.Failure(solarResult.Message,
                solarResult.Errors);

        var turbineResult = await assetService.GetTurbineByUserIdAsync(userId);
        if (!turbineResult.IsSuccess)
            return GeneralResponse<MonitoringAssetControl>.Failure(turbineResult.Message,
                turbineResult.Errors);

        var batteryResult = await assetService.GetBatteryByUserIdAsync(userId);
        if (!batteryResult.IsSuccess)
            return GeneralResponse<MonitoringAssetControl>.Failure(batteryResult.Message,
            batteryResult.Errors);


        var solarControl = assetControlFactory.CreateSolarControl(solarResult.Data);
        var turbineControl = assetControlFactory.CreateTurbineControl(turbineResult.Data);
        var batteryControl = assetControlFactory.CreateBatteryControl(batteryResult.Data);

        var operation = assetControlFactory.CreateAssetOperations(solarControl, turbineControl, batteryControl);

        var monitorAssetControl = new MonitoringAssetControl
        {
            SolarControl = solarControl,
            TurbineControl = turbineControl,
            BatteryControl = batteryControl,
            AssetOperations = operation
        };

        return GeneralResponse<MonitoringAssetControl>.Success(
            data: monitorAssetControl,
            message: "Monitoring asset is registration");
    }
}