using System.ComponentModel.DataAnnotations;

namespace Application.DTOs.User.Profile;

public record EditProfile
{
    [Required(ErrorMessage = "Username is required")]
    [StringLength(50, MinimumLength = 3,
        ErrorMessage = "Username must be between 3 and 50 characters")]
    [RegularExpression(@"^[a-zA-Z0-9_\-\.]+$",
        ErrorMessage = "Only letters, numbers, dots, underscores and hyphens allowed")]
    public string UserName { get; init; }
    public string? ProfileImage { get; set; }
}