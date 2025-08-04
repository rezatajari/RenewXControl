using System.ComponentModel.DataAnnotations;

namespace Application.DTOs.User.Auth;

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
    [Required(ErrorMessage = "Password is required.")]
    [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at most {1} characters long.", MinimumLength = 6)]
    [DataType(DataType.Password)]
    [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[\W_]).+$",
        ErrorMessage = "Password must include uppercase, lowercase, number, and special character.")]
    public string Password { get; init; }

    /// <summary>
    /// The confirmation of the password for validation.
    /// </summary>
    [Required]
    [Compare("Password", ErrorMessage = "Password and ConfirmPassword must match")]
    public string ConfirmPassword { get; init; }
}
