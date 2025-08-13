using System.ComponentModel.DataAnnotations;

namespace WebClient.DTOs.User.Auth
{
    public class ChangePassword
    {
        [Required(ErrorMessage = "Current password is required.")]
        public string CurrentPassword { get; set; }
        [Required(ErrorMessage = "New password is required.")]
        [MinLength(6, ErrorMessage = "New password must be at least 6 characters.")]
        public string NewPassword { get; set; }
        [Required(ErrorMessage = "please confirm your new password.")]
        [Compare(nameof(NewPassword), ErrorMessage = "The new password do not match.")]
        public string ConfirmNewPassword { get; set; }
    }
}
