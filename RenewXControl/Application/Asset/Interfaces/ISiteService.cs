using RenewXControl.Api.Utility;
using RenewXControl.Application.DTOs.AddAsset;

namespace RenewXControl.Application.Asset.Interfaces
{
    public interface ISiteService
    {
        Task<GeneralResponse<Guid>> AddSiteAsync(AddSite addSite,string userId);
        Task<GeneralResponse<Guid>> GetSiteId(string userId);
    }
}
