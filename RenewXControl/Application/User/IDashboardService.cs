using RenewXControl.Application.DTOs.User;

namespace RenewXControl.Application.User
{
    public interface IDashboardService
    {
        Task<Dashboard> GetDashboardDataAsync(string userId);
    }
}
