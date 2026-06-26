using Application.Interfaces.Asset;
using Domain.Entities;
using Domain.Entities.Assets;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services;

public class SiteRepository(RxcDbContext context) : ISiteRepository
{
    public async Task AddAsync(Site site)
    {
        await context.Sites.AddAsync(site);
    }

    public async Task<Site?> GetByIdAsync(Guid siteId)
    {
        return await context.Sites.FindAsync(siteId);
    }

    public async Task<Guid> GetIdAsync(Guid userId)
    {
        return await context.Sites
            .Where(s => s.UserId == userId)
            .Select(s => s.Id)
            .FirstOrDefaultAsync();
    }

    public async Task<bool> HasSite(Guid userId)
    {
        return await context.Sites
            .AnyAsync(s => s.UserId == userId);
    }

    public async Task<List<Site>> GetSitesAsync(Guid userId)
    {
        return await context.Sites.Where(u=>u.UserId==userId).ToListAsync();
    }

    public async Task<List<Asset>> GetAssetsBySite(Guid siteId)
    {
        return await context.Assets
            .Where(a => a.SiteId == siteId)
            .ToListAsync();
    }
}