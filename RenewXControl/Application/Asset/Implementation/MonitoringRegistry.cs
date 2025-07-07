using System.Collections.Concurrent;
using RenewXControl.Application.Asset.Interfaces;
using RenewXControl.Application.DTOs;
using RenewXControl.Domain.Interfaces.Assets;

namespace RenewXControl.Application.Asset.Implementation
{
    public class MonitoringRegistry : IMonitoringRegistry
    {
        private readonly Dictionary<string, MonitoringSession> _activeUsers = new();

        public void RegisterUser(string userId, ISolarControl solar, ITurbineControl turbine, IBatteryControl battery,IAssetRuntimeOperation assetRuntimeOperation)
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
