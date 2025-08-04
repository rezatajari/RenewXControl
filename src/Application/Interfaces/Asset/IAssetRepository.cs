using Domain.Entities.Assets;

namespace Application.Interfaces.Asset;

public interface IAssetRepository
{
    Task<int> GetTotalAssets(Guid userId);
    Task AddAssetAsync(Domain.Entities.Assets.Asset asset);
    Task<SolarPanel> GetSolarByUserId(Guid userId);
    Task<WindTurbine> GetTurbineByUserId(Guid userId);
    Task<Battery> GetBatteryByUserId(Guid userId);
}