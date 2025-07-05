namespace RenewXControl.Application.DTOs.User.Auth;

public record Register(
    string UserName,
    string Email,
    string Password,
    string ConfirmPassword
    );
