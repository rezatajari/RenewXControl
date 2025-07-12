using RenewXControl.Application.Asset.Interfaces.Asset;
using RenewXControl.Domain.Assets;
using RenewXControl.Domain.Implementatons.Assets;
using RenewXControl.Domain.Interfaces.Assets;

namespace RenewXControl.Application.Asset.Implementation.Asset
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
