using Application.Interfaces.Asset;
using Domain.Entities.Site;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services;

public class SiteRepository:ISiteRepository
{
    private readonly RxcDbContext _context;

    public SiteRepository(RxcDbContext context)
    {
        _context= context;
    }
    public async Task AddAsync(Domain.Entities.Site.Site site)
    {
        await _context.Sites.AddAsync(site);
    }

    public async Task<Domain.Entities.Site.Site?> GetByIdAsync(Guid siteId)
    {
        return await _context.Sites.FindAsync(siteId);
    }

    public async Task<Guid> GetIdAsync(Guid userId)
    {
        return await _context.Sites
            .Where(s => s.UserId == userId)
            .Select(s => s.Id)
            .FirstOrDefaultAsync();
    }

    public async Task<bool> HasSite(Guid userId)
    {
        return await _context.Sites
            .AnyAsync(s => s.UserId == userId);
    }

    public async Task<List<Site>> GetSitesAsync(Guid userId)
    {
        return await _context.Sites.Where(u=>u.UserId==userId).ToListAsync();
    }
}