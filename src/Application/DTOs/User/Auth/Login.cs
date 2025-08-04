using System.ComponentModel.DataAnnotations;

namespace Application.DTOs.User.Auth;

/// <summary>
/// Represents the login credentials for user authentication.
/// </summary>
public record Login
{
    /// <summary>
    /// The user's email address.
    /// </summary>
    [Required]
    [EmailAddress]
    public string Email { get; init; }

    /// <summary>
    /// The user's password.
    /// </summary>
    [Required]
    public string Password { get; init; }
}
