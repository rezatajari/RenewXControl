using Microsoft.EntityFrameworkCore;
using RenewXControl.Api.DTOs;
using RenewXControl.Application.Interfaces;
using RenewXControl.Infrastructure.Persistence.MyDbContext;
using Asset = RenewXControl.Domain.Assets.Asset;

namespace RenewXControl.Infrastructure.Services
{
    public class AssetRepository:IAssetRepository
    {
        private readonly RxcDbContext _context;
        public AssetRepository(RxcDbContext context)
        {
            _context = context;
        }

        public async Task<int> CountByUserIdAsync(string userId)
        {
        return await _context.Assets
                        .Where(a=>_context.Sites.Any(s=>s.Id==a.SiteId && s.UserId==userId))
                        .CountAsync();
        }

        public async Task AddAsync(Asset asset)
        {
           await _context.Assets.AddAsync(asset);
        }
    }
}
