using Microsoft.EntityFrameworkCore;
using RenewXControl.Application.Common;
using RenewXControl.Domain.Users;
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

        public async Task<List<Site>> GetSitesByUserIdAsync(string userId)
        {
            return await _context.Sites
                .Where(s => s.UserId== userId)
                .ToListAsync();
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
