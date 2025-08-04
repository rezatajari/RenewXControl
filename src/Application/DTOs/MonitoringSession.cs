using Application.Interfaces.Asset;
using Domain.Interfaces.Assets;

namespace Application.DTOs;

public record MonitoringSession(
    Guid UserId,
    ISolarControl SolarControl,
    ITurbineControl TurbineControl,
    IBatteryControl BatteryControl,
    IAssetOperations AssetRuntimeOperation
);