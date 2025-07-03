using RenewXControl.Application.DTOs;

namespace RenewXControl.Application.Interfaces
{
    public interface IDashboardService
    {
        Task<Dashboard> GetDashboardDataAsync(string userId);
    }
}
