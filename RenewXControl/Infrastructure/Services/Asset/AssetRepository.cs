using Microsoft.EntityFrameworkCore;
using RenewXControl.Application.Asset.Interfaces;
using RenewXControl.Infrastructure.Persistence;

namespace RenewXControl.Infrastructure.Services.Asset
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

        public async Task AddAsync(Domain.Assets.Asset asset)
        {
           await _context.Assets.AddAsync(asset);
        }
    }
}
