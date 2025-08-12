using Application.Common;
using Application.DTOs.User.Auth;

namespace Application.Interfaces.User;

public interface IAccountService
{
    Task<GeneralResponse<string>> RegisterAsync(Register register);
    Task<GeneralResponse<string>> LoginAsync(Login login);
    Task<GeneralResponse<bool>> LogoutAsync();
}
