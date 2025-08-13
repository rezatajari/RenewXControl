using Application.Common;
using Application.DTOs;
using Application.DTOs.User.Profile;

namespace Application.Interfaces.User;

public interface IUsersService
{
    Task<GeneralResponse<Profile>> GetProfile(Guid userId);
    Task<GeneralResponse<bool>> EditProfile(EditProfile editProfile, Guid userId);
    GeneralResponse<bool> ValidateUserId(Guid userId);
    Task<GeneralResponse<bool>> ChangePasswordAsync(ChangePassword changePassword, Guid userId);
    Task<GeneralResponse<string>> SaveProfileImageAsync(IUploadedFile file);
    Task<List<UserMonitoringInfo>> GetAllUsersWithSitesAndAssetsAsync();
}