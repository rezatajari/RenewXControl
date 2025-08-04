using Domain.Entities.Site;

namespace Application.Interfaces.Asset;

public interface ISiteRepository
{
    Task AddAsync(Site site);
    Task<Site?> GetByIdAsync(Guid siteId);
    Task<Guid> GetIdAsync(Guid userId);
    Task<bool> HasSite(Guid userId);
}