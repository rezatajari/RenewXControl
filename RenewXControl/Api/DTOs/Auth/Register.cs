namespace RenewXControl.Api.DTOs.Auth;

public record Register(
    string UserName,
    string Email,
    string Password,
    string ConfirmPassword
    );
