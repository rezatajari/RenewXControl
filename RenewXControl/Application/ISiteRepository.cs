using RenewXControl.Domain.Assets;

namespace RenewXControl.Application
{
    public interface ISiteRepository
    {
        Task AddAsync(Site site);
        Task<Site?> GetByIdAsync(Guid siteId);
        Task<Guid> GetIdAsync(string userId);
     }
}
