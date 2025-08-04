using Application.DTOs;
using Application.Interfaces.Asset;
using Domain.Interfaces.Assets;

namespace Application.Interfaces.Monitoring;

public interface IMonitoringRegistry
{
    void RegisterUser(Guid userId, ISolarControl solar, ITurbineControl turbine, IBatteryControl battery,IAssetOperations assetRuntimeOperation);
    bool IsUserRegistered(Guid userId);
    MonitoringSession? GetUserSession(Guid userId);
    IEnumerable<MonitoringSession> GetAllSessions();
}