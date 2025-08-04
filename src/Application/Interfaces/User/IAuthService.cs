using Application.Common;
using Application.DTOs.User.Auth;

namespace Application.Interfaces.User;

public interface IAuthService
{
    Task<GeneralResponse<string>> RegisterAsync(Register register);
    Task<GeneralResponse<string>> LoginAsync(Login login);
    Task<GeneralResponse<bool>> LogoutAsync();
    string GenerateToken(AuthUser user, IList<string> roles);
    Task<GeneralResponse<bool>> ChangePasswordAsync(ChangePassword  changePassword,Guid userId);
}