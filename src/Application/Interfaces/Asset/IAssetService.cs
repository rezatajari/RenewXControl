using Application.Common;
using Application.DTOs.AddAsset;
using Domain.Entities.Assets;

namespace Application.Interfaces.Asset;

public interface IAssetService
{
    Task<GeneralResponse<Guid>> AddBattery(Guid userId, AddBattery addBattery,Guid siteId);
    Task<GeneralResponse<Guid>> AddSolar(Guid userId, AddSolar addSolar, Guid siteId);
    Task<GeneralResponse<Guid>> AddTurbine(Guid userId, AddTurbine addTurbine, Guid siteId);
    Task<GeneralResponse<SolarPanel>> GetSolarByUserIdAsync(Guid userId);
    Task<GeneralResponse<WindTurbine>> GetTurbineByUserIdAsync(Guid userId);
    Task<GeneralResponse<Battery>> GetBatteryByUserIdAsync(Guid userId);
}