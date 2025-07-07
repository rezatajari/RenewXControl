using RenewXControl.Application.DTOs;
using RenewXControl.Domain.Interfaces.Assets;

namespace RenewXControl.Application.Asset.Interfaces
{
    public interface IMonitoringRegistry
    {
            void RegisterUser(string userId, ISolarControl solar, ITurbineControl turbine, IBatteryControl battery,IAssetRuntimeOperation assetRuntimeOperation);
            bool IsUserRegistered(string userId);
            MonitoringSession? GetUserSession(string userId);
            IEnumerable<MonitoringSession> GetAllSessions();
    }
}
