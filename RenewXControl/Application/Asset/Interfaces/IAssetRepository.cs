using RenewXControl.Api.Utility;
using RenewXControl.Domain.Assets;

namespace RenewXControl.Application.Asset.Interfaces
{
    public interface IAssetRepository
    {
        Task<int> CountByUserIdAsync(string userId);
        Task AddAssetAsync(Domain.Assets.Asset asset);
        Task AddSite(Site site);
        Task<Domain.Assets.Site> GetSiteById(Guid siteId);
        Task<Domain.Assets.SolarPanel> GetSolarById(Guid  solarId);
        Task<Domain.Assets.WindTurbine> GetTurbineById(Guid turbineId);
        Task<Domain.Assets.Battery> GetBatteryById(Guid batteryId);
    }
}
