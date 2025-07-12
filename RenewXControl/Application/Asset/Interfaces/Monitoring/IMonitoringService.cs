using RenewXControl.Api.Utility;

namespace RenewXControl.Application.Asset.Interfaces.Monitoring
{
    public interface IMonitoringService
    {
        Task<GeneralResponse<bool>> RegisterMonitoringSession(string userId);
    }
}
