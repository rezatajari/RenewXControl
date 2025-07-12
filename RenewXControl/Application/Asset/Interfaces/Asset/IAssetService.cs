using RenewXControl.Api.Utility;
using RenewXControl.Application.DTOs.AddAsset;
using RenewXControl.Application.DTOs.AssetMonitoring;
using RenewXControl.Domain.Assets;
using Battery = RenewXControl.Domain.Assets.Battery;

namespace RenewXControl.Application.Asset.Interfaces.Asset
{
    public interface IAssetService
    {
        Task<GeneralResponse<Guid>> AddBatteryAsync(AddBattery addBattery,string userId);
        Task<GeneralResponse<Guid>> AddSolarAsync(AddSolar addSolar,string userId);
        Task<GeneralResponse<Guid>> AddTurbineAsync(AddTurbine addTurbine, string userId);
        Task<GeneralResponse<SolarPanel>> GetSolarByUserIdAsync(string userId);
        Task<GeneralResponse<WindTurbine>> GetTurbineByUserIdAsync(string userId);
        Task<GeneralResponse<Battery>> GetBatteryByUserIdAsync(string userId);
    }
}
