using System.Collections.Concurrent;
using RenewXControl.Application.Asset.Interfaces.Asset;
using RenewXControl.Application.Asset.Interfaces.Monitoring;
using RenewXControl.Application.DTOs;
using RenewXControl.Domain.Interfaces.Assets;

namespace RenewXControl.Application.Asset.Implementation.Monitoring
{
    public class MonitoringRegistry : IMonitoringRegistry
    {
        private readonly Dictionary<string, MonitoringSession> _activeUsers = new();

        public void RegisterUser(string userId, ISolarControl solar, ITurbineControl turbine, IBatteryControl battery,IAssetOperations assetRuntimeOperation)
        {
            _activeUsers[userId] = new MonitoringSession(userId, solar, turbine, battery,assetRuntimeOperation);
        }

        public bool IsUserRegistered(string userId)
        {
            return _activeUsers.ContainsKey(userId);
        }

        public MonitoringSession? GetUserSession(string userId)
        {
            return _activeUsers.GetValueOrDefault(userId);
        }

        public IEnumerable<MonitoringSession> GetAllSessions()
        {
            return _activeUsers.Values;
        }
    }
}
