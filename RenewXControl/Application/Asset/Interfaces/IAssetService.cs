using RenewXControl.Api.Utility;
using RenewXControl.Application.DTOs.AddAsset;
using RenewXControl.Application.DTOs.AssetMonitoring;

namespace RenewXControl.Application.Asset.Interfaces
{
    public interface IAssetService
    {
        Task<GeneralResponse<Guid>> AddBatteryAsync(AddBattery addBattery,string userId);
        Task<GeneralResponse<Guid>> AddSolarAsync(AddSolar addSolar,string userId);
        Task<GeneralResponse<Guid>> AddTurbineAsync(AddTurbine addTurbine, string userId);
        Task MonitoringData(string userId);
    }
}
