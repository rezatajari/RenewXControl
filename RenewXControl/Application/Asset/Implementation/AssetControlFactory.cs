using RenewXControl.Application.Asset.Interfaces;
using RenewXControl.Domain.Assets;
using RenewXControl.Domain.Implementatons.Assets;
using RenewXControl.Domain.Interfaces.Assets;

namespace RenewXControl.Application.Asset.Implementation
{
    public class AssetControlFactory : IAssetControlFactory
    {
        private readonly IAssetRepository _assetRepository;

        public AssetControlFactory(IAssetRepository assetRepository)
        {
            _assetRepository = assetRepository;
        }

        public ISolarControl CreateSolarControlAsync(SolarPanel solar)
        {
            if (solar == null)
                throw new InvalidOperationException("Invalid or missing solar asset.");

            return new SolarControl(solar);
        }

        public ITurbineControl CreateTurbineControlAsync(WindTurbine turbine)
        {
            if (turbine == null)
                throw new InvalidOperationException("Invalid or missing turbine asset.");

            return new TurbineControl(turbine);
        }

        public IBatteryControl CreateBatteryControlAsync(Battery battery)
        {
            if (battery == null)
                throw new InvalidOperationException("Invalid or missing battery asset.");

            return new BatteryControl(battery);
        }

        public IAssetRuntimeOperation CreateAssetRuntimeOperationAsync(ISolarControl solarControl, ITurbineControl turbineControl,
            IBatteryControl batteryControl)
        {
            return new AssetRuntimeOperation(batteryControl, solarControl, turbineControl);
        }
    }
}
