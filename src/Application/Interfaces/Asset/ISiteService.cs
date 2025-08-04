using Application.Common;
using Application.DTOs.AddAsset;

namespace Application.Interfaces.Asset;

public interface ISiteService
{
    Task<GeneralResponse<Guid>> AddSiteAsync(AddSite addSite,Guid userId);
    Task<GeneralResponse<Guid>> GetSiteId(Guid userId);
    Task<GeneralResponse<bool>> HasSiteAsync(Guid userId);
}