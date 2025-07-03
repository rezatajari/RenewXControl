using RenewXControl.Api.DTOs.Auth;
using RenewXControl.Api.Utility;

namespace RenewXControl.Application.Interfaces
{
    public interface IAuthService
    {
        Task<GeneralResponse<string>> RegisterAsync(Register register);
        Task<GeneralResponse<string>> LoginAsync(Login login);
    }
}
