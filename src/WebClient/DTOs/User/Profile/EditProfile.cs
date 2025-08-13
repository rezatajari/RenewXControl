
using System.ComponentModel.DataAnnotations;

namespace WebClient.DTOs.User.Profile;

public class EditProfile
{
    [Required(ErrorMessage = "Username is required")]
    [StringLength(50, MinimumLength = 3,
        ErrorMessage = "Username must be between 3 and 50 characters")]
    [RegularExpression(@"^[a-zA-Z0-9_\-\.]+$",
        ErrorMessage = "Only letters, numbers, dots, underscores and hyphens allowed")]
    [Display(Name = "Username", Prompt = "Enter your username")]
    public string UserName { get; set; }
    public string? ProfileImage { get; set; }
   
}