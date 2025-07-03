using RenewXControl.Api.Utility;
using RenewXControl.Application.DTOs.Asset;

namespace RenewXControl.Application.Interfaces
{
    public interface IAssetSettingService
    {
        Task<GeneralResponse<Guid>> AddBatteryAsync(AddBattery addBattery, string userId);
        Task<GeneralResponse<Guid>> AddBatteryAsync(AddSolar addSolar, string userId);
        Task<GeneralResponse<Guid>> AddBatteryAsync(AddTurbine addTurbine, string userId);
    }
}
