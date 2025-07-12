using RenewXControl.Domain.Assets;
using RenewXControl.Domain.Interfaces.Assets;

namespace RenewXControl.Application.Asset.Interfaces.Asset
{
    public interface IAssetControlFactory
    {
        ISolarControl CreateSolarControl(SolarPanel solar);
        ITurbineControl CreateTurbineControl(WindTurbine turbine);
        IBatteryControl CreateBatteryControl(Battery battery);
        IAssetOperations CreateAssetOperations(ISolarControl solarControl,ITurbineControl turbineControl,IBatteryControl batteryControl);
    }
}
