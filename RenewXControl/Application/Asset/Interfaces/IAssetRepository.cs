using RenewXControl.Api.Utility;
using RenewXControl.Domain.Assets;

namespace RenewXControl.Application.Asset.Interfaces
{
    public interface IAssetRepository
    {
        Task<int> GetTotalAssets(string userId);
        Task AddAssetAsync(Domain.Assets.Asset asset);
        Task<SolarPanel> GetSolarByUserId(string userId);
        Task<WindTurbine> GetTurbineByUserId(string userId);
        Task<Battery> GetBatteryByUserId(string userId);
    }
}
