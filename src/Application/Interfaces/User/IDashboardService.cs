using Application.Common;
using Application.DTOs.User.Profile;

namespace Application.Interfaces.User;

public interface IDashboardService
{
    Task<GeneralResponse<Profile>> GetProfile(Guid userId);
    Task<GeneralResponse<bool>> EditProfile(EditProfile editProfile, Guid userId);
}