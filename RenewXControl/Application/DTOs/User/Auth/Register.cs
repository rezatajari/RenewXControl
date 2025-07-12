using System.ComponentModel.DataAnnotations;

namespace RenewXControl.Application.DTOs.User.Auth;

/// <summary>
/// Represents the registration data required to create a new user account.
/// </summary>
public record Register
{
    /// <summary>
    /// The desired username for the new user.
    /// </summary>
    [Required]
    public string UserName { get; init; }

    /// <summary>
    /// The email address for the new user.
    /// </summary>
    [Required]
    [EmailAddress]
    public string Email { get; init; }

    /// <summary>
    /// The password for the new user account.
    /// </summary>
    [Required]
    [MinLength(6)]
    public string Password { get; init; }

    /// <summary>
    /// The confirmation of the password for validation.
    /// </summary>
    [Required]
    [Compare("Password", ErrorMessage = "Password and ConfirmPassword must match")]
    public string ConfirmPassword { get; init; }
}
