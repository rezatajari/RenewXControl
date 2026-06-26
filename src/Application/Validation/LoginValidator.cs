using Application.DTOs.User.Auth;
using FluentValidation;

namespace Application.Validation;

public sealed class LoginValidator : AbstractValidator<Login>
{
    public LoginValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty()
            .EmailAddress();

        RuleFor(x => x.Password)
            .NotEmpty();
    }
}
