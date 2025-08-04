using Application.Interfaces.Asset;
using Domain.Entities.Assets;
using Domain.Implementations.Assets;
using Domain.Interfaces.Assets;

namespace Application.Implementations.Asset
{
    public class AssetControlFactory : IAssetControlFactory
    {
        public ISolarControl CreateSolarControl(SolarPanel solar)
        {
            return new SolarControl(solar);
        }

        public ITurbineControl CreateTurbineControl(WindTurbine turbine)
        {
            return new TurbineControl(turbine);
        }

        public IBatteryControl CreateBatteryControl(Battery battery)
        {
            return new BatteryControl(battery);
        }

        public IAssetOperations CreateAssetOperations(ISolarControl solarControl, ITurbineControl turbineControl,
            IBatteryControl batteryControl)
        {
            return new AssetOperations(batteryControl, solarControl, turbineControl);
        }
    }
}
