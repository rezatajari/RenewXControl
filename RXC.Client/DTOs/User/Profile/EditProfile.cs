namespace RXC.Client.DTOs.User.Profile;

using System.ComponentModel.DataAnnotations;

public class EditProfile
{
    [Required(ErrorMessage = "Username is required")]
    [StringLength(50, MinimumLength = 3,
        ErrorMessage = "Username must be between 3 and 50 characters")]
    [RegularExpression(@"^[a-zA-Z0-9_\-\.]+$",
        ErrorMessage = "Only letters, numbers, dots, underscores and hyphens allowed")]
    [Display(Name = "Username", Prompt = "Enter your username")]
    public string UserName { get; set; }

    [Url(ErrorMessage = "Please enter a valid image URL")]
    [StringLength(500,
        ErrorMessage = "Image URL cannot exceed 500 characters")]
    [Display(Name = "Profile Image URL",
        Prompt = "https://example.com/your-image.jpg")]
    public string? ProfileImage { get; set; }
   
}