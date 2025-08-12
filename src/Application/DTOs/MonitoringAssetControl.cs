using Application.Interfaces.Asset;
using Domain.Interfaces.Assets;

namespace Application.DTOs;

public class MonitoringAssetControl
{
    public ISolarControl SolarControl { get; set; }
    public ITurbineControl TurbineControl{ get; set; }
    public IBatteryControl BatteryControl{ get; set; }
    public IAssetOperations AssetOperations { get; set; }
}