using System.ComponentModel.DataAnnotations;

namespace RenewXControl.Application.DTOs.User.Auth;

public record Login
{
    [Required]
    [EmailAddress]
    public string Email { get; set; }
    [Required]
    public string Password { get; set; }
}