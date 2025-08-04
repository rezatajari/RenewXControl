using RenewXControl.Api.Utility;
using RenewXControl.Application.DTOs.User.Profile;

namespace RenewXControl.Application.User.Interfaces
{
    public interface IDashboardService
    {
        Task<GeneralResponse<Profile>> GetProfile(string userId);
        Task<GeneralResponse<bool>> EditProfile(EditProfile editProfile,string userId);
    }
}
