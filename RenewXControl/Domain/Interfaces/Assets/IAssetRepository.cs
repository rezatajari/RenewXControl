using RenewXControl.Api.Utility;
using RenewXControl.Domain.Assets;

namespace RenewXControl.Domain.Interfaces.Assets
{
    public interface IAssetRepository
    {
        Task<int> CountByUserIdAsync(string userId);
        Task AddAsync(Asset asset);
    }
}
