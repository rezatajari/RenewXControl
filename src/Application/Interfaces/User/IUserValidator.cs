using Application.Common;

namespace Application.Interfaces.User;

public interface IUserValidator
{
    GeneralResponse<bool> ValidateUserId(Guid userId);
}