using System.ComponentModel.DataAnnotations;

namespace RXC.Client.DTOs.User.Auth
{
    public class Login
    {
        [Required]
        [EmailAddress]
        public string Email { get; init; }

        [Required]
        public string Password { get; init; }

    }
}
