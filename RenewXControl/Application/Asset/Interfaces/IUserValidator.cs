using RenewXControl.Api.Utility;

namespace RenewXControl.Application.Asset.Interfaces;

public interface IUserValidator
{
    GeneralResponse<bool> ValidateUserId(string userId);
}