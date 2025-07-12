using RenewXControl.Api.Utility;
using RenewXControl.Application.Asset.Interfaces;
using RenewXControl.Application.DTOs.AddAsset;
using RenewXControl.Domain.Assets;
using RenewXControl.Infrastructure.Persistence;

namespace RenewXControl.Application.Asset.Implementation
{
    public class SiteService:ISiteService
    {
        private readonly ISiteRepository _siteRepository;
        private readonly RxcDbContext _context;

        public SiteService(ISiteRepository siteRepository, RxcDbContext context)
        {
            _siteRepository=siteRepository;
            _context = context;
        }
        public async Task<GeneralResponse<Guid>> AddSiteAsync(AddSite addSite, string userId)
        {
            if (string.IsNullOrWhiteSpace(userId))
            {
                return GeneralResponse<Guid>.Failure(
                    message: "User detection failed",
                    errors: [
                        new ErrorResponse
                        {
                            Name = "User credential",
                            Message = "User ID is required"
                        }
                    ]);
            }

            var site = Site.Create(addSite, userId);
            await _siteRepository.AddAsync(site);

            await _context.SaveChangesAsync();

            return GeneralResponse<Guid>.Success(
                data: site.Id,
                message:"Site added successfully"
                );
        }

        public async Task<GeneralResponse<Guid>> GetSiteId(string userId)
        {
            if (string.IsNullOrWhiteSpace(userId))
            {
                return GeneralResponse<Guid>.Failure(
                    message:"User detection failed",
                    errors: [
                        new ErrorResponse
                        {
                            Name = "User credential",
                            Message = "User ID is required"
                        }
                        ]);
            }

            var siteId = await _siteRepository.GetIdAsync(userId);
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
    }
}
