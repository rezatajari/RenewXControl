using RenewXControl.Application.Asset.Interfaces;
using RenewXControl.Domain.Assets;
using RenewXControl.Domain.Implementatons.Assets;
using RenewXControl.Domain.Interfaces.Assets;

namespace RenewXControl.Application.Asset.Implementation
{
    public class AssetFactory:IAssetFactory
    {
        private readonly IAssetRepository _assetRepository;

        public AssetFactory(IAssetRepository assetRepository)
        {
            _assetRepository= assetRepository;
        }

        public async Task<ISolarControl?> CreateSolarControlAsync(Guid assetId)
        {
            var solar= await _assetRepository.GetSolarById(assetId);

            if (solar == null)
                throw new InvalidOperationException("Invalid or missing solar asset.");

            return new SolarControl(solar);
        }

        public async Task<ITurbineControl> CreateTurbineControlAsync(Guid turbineId)
        {
            var turbine = await _assetRepository.GetTurbineById(turbineId);

            if (turbine == null)
                throw new InvalidOperationException("Invalid or missing turbine asset.");

            return new TurbineControl(turbine);
        }

        public async Task<IBatteryControl> CreateBatteryControlAsync(Guid batteryId)
        {
            var battery = await _assetRepository.GetBatteryById(batteryId);

            if (battery == null)
                throw new InvalidOperationException("Invalid or missing battery asset.");

            return new BatteryControl(battery);
        }
    }
}
