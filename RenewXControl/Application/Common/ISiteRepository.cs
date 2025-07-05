using RenewXControl.Domain.Users;

namespace RenewXControl.Application.Common
{
    public interface ISiteRepository
    {
        Task<Site?> GetByIdAsync(Guid siteId);
        Task<List<Site>> GetSitesByUserIdAsync(string userId);
        Task AddAsync(Site site);
        Task<bool> ExistAsync(Guid siteId);
    }
}
