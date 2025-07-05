using RenewXControl.Domain.Assets;

namespace RenewXControl.Application
{
    public interface ISiteRepository
    {
        Task<Site?> GetByIdAsync(Guid siteId);
        Task<Guid> GetSiteIdByUserIdAsync(string userId);
        Task AddAsync(Site site);
        Task<bool> ExistAsync(Guid siteId);
    }
}
