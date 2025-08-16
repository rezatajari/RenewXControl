using System.ComponentModel.DataAnnotations;

namespace WebClient.DTOs.User.Auth;

public class Register
{
    [Required]
    public string UserName { get; set; }

    [Required]
    [EmailAddress]
    public string Email { get; set; }

    [Required(ErrorMessage = "Password is required.")]
    [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at most {1} characters long.", MinimumLength = 6)]
    [DataType(DataType.Password)]
    [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[\W_]).+$",
        ErrorMessage = "Password must include uppercase, lowercase, number, and special character.")]
    public string Password { get; set; }

    [Required]
    [Compare("Password", ErrorMessage = "Password and ConfirmPassword must match")]
    public string ConfirmPassword { get; set; }
}
