using Application.Common;
using Application.DTOs.AddAsset;
using Application.Interfaces.Asset;
using Application.Interfaces.User;
using Domain.Entities.Site;

namespace Application.Implementations.Asset;

public class SiteService(ISiteRepository siteRepository, IUnitOfWork unitOfWork)
    : ISiteService
{
    public async Task<GeneralResponse<Guid>> AddSite(AddSite addSite, Guid userId)
    {
        var site = Site.Create(addSite.Name,addSite.Location, userId);
        await siteRepository.AddAsync(site);

        await unitOfWork.SaveChangesAsync();

        return GeneralResponse<Guid>.Success(
            data: site.Id,
            message:"Site added successfully"
        );
    }

    public async Task<GeneralResponse<List<DTOs.Site>>> GetSites(Guid userId)
    {
        var response= await siteRepository.GetSitesAsync(userId);
        if (response.Count == 0)
        {
            return GeneralResponse<List<DTOs.Site>>.Failure(
                message: "No sites found",
                errors: [
                    new ErrorResponse
                    {
                        Name = "GetSites",
                        Message = "The user does not own any site"
                    }
                ]);
        }

        var sites = response.Select(site => new DTOs.Site(site.Id, site.Name, site.Location)).ToList();
        return GeneralResponse<List<DTOs.Site>>.Success(
            data:sites,
            message: "Sites retrieved successfully");

    }

    public async Task<GeneralResponse<Guid>> GetSiteId(Guid userId)
    {
        var siteId = await siteRepository.GetIdAsync(userId);
        if (siteId == Guid.Empty)
        {
            return GeneralResponse<Guid>.Failure(
                message:"Site Unfounded",
                errors: [
                    new ErrorResponse
                    {
                        Name = "Site",
                        Message = "The user does not own any site"
                    }
                ]);
        }

        return GeneralResponse<Guid>.Success(siteId);
    }

    public async Task<GeneralResponse<bool>> HasSite(Guid userId)
    {
        var response =await siteRepository.HasSite(userId);
        if (response)
            return GeneralResponse<bool>.Success(data: true, message: "Has site operation is successful");

        return GeneralResponse<bool>.Failure(
            message:"Site NotFound",
            errors: [
                new ErrorResponse
                {
                    Name = "HasSite",
                    Message = "Your site is not created until now"
                }
            ]);
    }

    public async Task<GeneralResponse<bool>> CanAddAsset(Guid siteId, Type assetType)
    {
        // Get existing assets for the site
        var assets = await siteRepository.GetAssetsBySite(siteId);

        // Check if this asset type already exists
        var exists = assets.Any(a => a.GetType() == assetType);
        if (exists)
            return GeneralResponse<bool>.Failure(
                message: $"You already have a {assetType.Name} for this site",
                errors:
                [
                    new ErrorResponse
                    {
                        Name = assetType.Name,
                        Message = $"Only one {assetType.Name} allowed per site"
                    }
                ]);

        return GeneralResponse<bool>.Success(true, "Can add asset");
    }
}