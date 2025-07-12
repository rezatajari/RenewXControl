using RenewXControl.Application.Asset.Interfaces.Asset;
using RenewXControl.Domain.Interfaces.Assets;

namespace RenewXControl.Application.DTOs;

public record MonitoringSession(
    string UserId,
    ISolarControl SolarControl,
    ITurbineControl TurbineControl,
    IBatteryControl BatteryControl,
    IAssetOperations AssetRuntimeOperation
);