using Microsoft.EntityFrameworkCore;
using RenewXControl.Application;
using RenewXControl.Domain.Assets;
using RenewXControl.Infrastructure.Persistence;

namespace RenewXControl.Infrastructure.Services
{
    public class SiteRepository:ISiteRepository
    {
        private readonly RxcDbContext _context;

        public SiteRepository(RxcDbContext context)
        {
            _context= context;
        }
        public async Task<Site?> GetByIdAsync(Guid siteId)
        {
            return await _context.Sites.FindAsync(siteId);
        }

        public async Task<Guid> GetSiteIdByUserIdAsync(string userId)
        {
            return await _context.Sites
                .Where(s => s.UserId == userId)
                .Select(s => (Guid)s.Id)
                .FirstOrDefaultAsync();
        }

        public async Task AddAsync(Site site)
        {
            await _context.Sites.AddAsync(site);
        }

        public async Task<bool> ExistAsync(Guid siteId)
        {
            return await _context.Sites.AnyAsync(s => s.Id == siteId);
        }
    }
}
