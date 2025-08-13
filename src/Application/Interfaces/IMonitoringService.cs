using Application.Common;
using Application.DTOs;
using Domain.Entities.Assets;

namespace Application.Interfaces;

public interface IMonitoringService
{
    Task<GeneralResponse<List<UserMonitoringInfo>>> GetAllUsersWithSitesAndAssets();
    Task ChargeDischarge(SolarPanel solar,WindTurbine turbine,Battery battery);
}