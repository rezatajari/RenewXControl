using RenewXControl.Application.DTOs.User;

namespace RenewXControl.Application.User
{
    public interface IDashboardService
    {
        Task<Profile> GetDashboardDataAsync(string userId);
    }
}
