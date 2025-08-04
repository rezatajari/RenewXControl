using Domain.Entities.Assets;
using Domain.Interfaces.Assets;

namespace Application.Interfaces.Asset
{
    public interface IAssetControlFactory
    {
        ISolarControl CreateSolarControl(SolarPanel solar);
        ITurbineControl CreateTurbineControl(WindTurbine turbine);
        IBatteryControl CreateBatteryControl(Battery battery);
        IAssetOperations CreateAssetOperations(ISolarControl solarControl,ITurbineControl turbineControl,IBatteryControl batteryControl);
    }
}
