using Application.Common;
using Application.DTOs;
using Application.Interfaces.Asset;
using Domain.Interfaces.Assets;

namespace Application.Interfaces.Monitoring;

public interface IMonitoringService
{
    Task<GeneralResponse<MonitoringAssetControl>> MonitoringAssetControl(Guid userId);
}