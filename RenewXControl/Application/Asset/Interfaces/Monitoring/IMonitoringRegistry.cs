using RenewXControl.Application.Asset.Interfaces.Asset;
using RenewXControl.Application.DTOs;
using RenewXControl.Domain.Interfaces.Assets;

namespace RenewXControl.Application.Asset.Interfaces.Monitoring
{
    public interface IMonitoringRegistry
    {
            void RegisterUser(string userId, ISolarControl solar, ITurbineControl turbine, IBatteryControl battery,IAssetOperations assetRuntimeOperation);
            bool IsUserRegistered(string userId);
            MonitoringSession? GetUserSession(string userId);
            IEnumerable<MonitoringSession> GetAllSessions();
    }
}
