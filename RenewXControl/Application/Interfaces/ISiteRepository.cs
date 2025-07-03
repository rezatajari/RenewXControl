using RenewXControl.Domain.Users;

namespace RenewXControl.Application.Interfaces
{
    public interface ISiteRepository
    {
        Task<Site?> GetByIdAsync(Guid siteId);
        Task<List<Site>> GetSitesByUserIdAsync(string userId);
        Task AddAsync(Site site);
        Task<bool> ExistAsync(Guid siteId);
    }
}
