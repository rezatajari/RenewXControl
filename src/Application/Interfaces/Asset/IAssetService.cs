using Application.Common;
using Application.DTOs.AddAsset;
using Domain.Entities.Assets;

namespace Application.Interfaces.Asset;

public interface IAssetService
{
    Task<GeneralResponse<Guid>> AddBatteryAsync(AddBattery addBattery,Guid userId);
    Task<GeneralResponse<Guid>> AddSolarAsync(AddSolar addSolar, Guid userId);
    Task<GeneralResponse<Guid>> AddTurbineAsync(AddTurbine addTurbine, Guid userId);
    Task<GeneralResponse<SolarPanel>> GetSolarByUserIdAsync(Guid userId);
    Task<GeneralResponse<WindTurbine>> GetTurbineByUserIdAsync(Guid userId);
    Task<GeneralResponse<Battery>> GetBatteryByUserIdAsync(Guid userId);
}