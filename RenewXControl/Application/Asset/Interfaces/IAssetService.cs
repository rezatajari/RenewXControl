using RenewXControl.Api.Utility;
using RenewXControl.Application.DTOs.AddAsset;

namespace RenewXControl.Application.Asset.Interfaces
{
    public interface IAssetService
    {
        Task<GeneralResponse<Guid>> AddSiteAsync(AddSite addSite,string userId);
        Task<GeneralResponse<Guid>> AddBatteryAsync(AddBattery addBattery,Guid siteId);
        Task<GeneralResponse<Guid>> AddSolarAsync(AddSolar addSolar, Guid siteId);
        Task<GeneralResponse<Guid>> AddTurbineAsync(AddTurbine addTurbine, Guid siteId);
    }
}
