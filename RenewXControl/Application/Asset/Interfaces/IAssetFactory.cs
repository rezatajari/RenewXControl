using RenewXControl.Domain.Interfaces.Assets;

namespace RenewXControl.Application.Asset.Interfaces
{
    public interface IAssetFactory
    {
        Task<ISolarControl?> CreateSolarControlAsync(Guid assetId);
        Task<ITurbineControl> CreateTurbineControlAsync(Guid turbineId);
        Task<IBatteryControl> CreateBatteryControlAsync(Guid batteryId);
    }

}
