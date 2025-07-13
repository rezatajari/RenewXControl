using Microsoft.EntityFrameworkCore;
using RenewXControl.Application;
using RenewXControl.Domain;
using RenewXControl.Infrastructure.Persistence;

namespace RenewXControl.Infrastructure.Services.Site
{
    public class SiteRepository:ISiteRepository
    {
        private readonly RxcDbContext _context;

        public SiteRepository(RxcDbContext context)
        {
            _context= context;
        }
        public async Task AddAsync(Domain.Site.Site site)
        {
            await _context.Sites.AddAsync(site);
        }

        public async Task<Domain.Site.Site?> GetByIdAsync(Guid siteId)
        {
            return await _context.Sites.FindAsync(siteId);
        }

        public async Task<Guid> GetIdAsync(string userId)
        {
            return await _context.Sites
                .Where(s => s.UserId == userId)
                .Select(s => s.Id)
                .FirstOrDefaultAsync();
        }

        public async Task<bool> HasSite(string userId)
        {
           return await _context.Sites
                .AnyAsync(s => s.UserId == userId);
        }
    }
}
