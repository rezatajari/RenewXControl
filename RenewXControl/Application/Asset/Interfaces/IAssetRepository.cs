using RenewXControl.Api.Utility;
using RenewXControl.Domain.Assets;

namespace RenewXControl.Application.Asset.Interfaces
{
    public interface IAssetRepository
    {
        Task<int> CountByUserIdAsync(string userId);
        Task AddAsync(Domain.Assets.Asset asset);
    }
}
