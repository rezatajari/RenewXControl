using System.ComponentModel.DataAnnotations;

namespace Application.DTOs.User.Auth;

public record Login
{
    public string Email { get; init; } = string.Empty;
    public string Password { get; init; } = string.Empty;
}