using System.ComponentModel.DataAnnotations;

namespace RXC.Client.DTOs.User.Auth;

public record Register
{
    [Required]
    public string UserName { get; init; }

    [Required]
    [EmailAddress]
    public string Email { get; init; }

    [Required]
    [MinLength(6)]
    public string Password { get; init; }

    [Required]
    [Compare("Password", ErrorMessage = "Password and ConfirmPassword must match")]
    public string ConfirmPassword { get; init; }
}
