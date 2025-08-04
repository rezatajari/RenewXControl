using Application.Common;

namespace Application.Interfaces.Monitoring;

public interface IMonitoringService
{
    Task<GeneralResponse<bool>> RegisterMonitoringSession(Guid userId);
}