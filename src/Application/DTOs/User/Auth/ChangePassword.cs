using System.ComponentModel.DataAnnotations;

namespace Application.DTOs.User.Auth;

public record ChangePassword
{
    [Required(ErrorMessage = "Current password is required.")]
    public string CurrentPassword { get; init; }
    [Required(ErrorMessage = "New password is required.")]
    [MinLength(6,ErrorMessage = "New password must be at least 6 characters.")]
    public string NewPassword { get; init; }
    [Required(ErrorMessage = "please confirm your new password.")]
    [Compare(nameof(NewPassword),ErrorMessage = "The new password do not match.")]
    public string ConfirmNewPassword { get; init; }
}