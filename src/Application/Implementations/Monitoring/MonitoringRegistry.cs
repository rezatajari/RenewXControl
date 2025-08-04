using Application.DTOs;
using Application.Interfaces.Asset;
using Application.Interfaces.Monitoring;
using Domain.Interfaces.Assets;

namespace Application.Implementations.Monitoring;

public class MonitoringRegistry : IMonitoringRegistry
{
    private readonly Dictionary<Guid, MonitoringSession> _activeUsers = new();

    public void RegisterUser(Guid userId, ISolarControl solar, ITurbineControl turbine, IBatteryControl battery,
        IAssetOperations assetRuntimeOperation)
    {
        _activeUsers[userId] = new MonitoringSession(userId, solar, turbine, battery,assetRuntimeOperation);
    }

    public bool IsUserRegistered(Guid userId)
    {
        return _activeUsers.ContainsKey(userId);
    }

    public MonitoringSession? GetUserSession(Guid userId)
    {
        return _activeUsers.GetValueOrDefault(userId);
    }

    public IEnumerable<MonitoringSession> GetAllSessions()
    {
        return _activeUsers.Values;
    }
}