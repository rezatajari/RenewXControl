using RenewXControl.Api.Utility;
using RenewXControl.Application.DTOs.User;

namespace RenewXControl.Application.User
{
    public interface IDashboardService
    {
        Task<GeneralResponse<Profile>> GetProfile(string userId);
    }
}
