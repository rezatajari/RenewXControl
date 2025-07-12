using RenewXControl.Api.Utility;
using RenewXControl.Application.DTOs.User.Auth;

namespace RenewXControl.Application.User.Interfaces
{
    public interface IAuthService
    {
        Task<GeneralResponse<string>> RegisterAsync(Register register);
        Task<GeneralResponse<string>> LoginAsync(Login login);
        string GenerateToken(Domain.User.User  user, IList<string> roles);
    }
}
