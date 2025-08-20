using Application.Common;
using Application.DTOs.AddAsset;
using Domain.Entities.Site;
using Site = Application.DTOs.Site;

namespace Application.Interfaces.Asset;

public interface ISiteService
{
    Task<GeneralResponse<Guid>> AddSite(AddSite addSite,Guid userId);
    Task<GeneralResponse<List<Site>>> GetSites(Guid userId);
    Task<GeneralResponse<Guid>> GetSiteId(Guid userId);
    Task<GeneralResponse<bool>> HasSite(Guid userId);
    Task<GeneralResponse<bool>> CanAddAsset(Guid siteId, Type assetType);
}