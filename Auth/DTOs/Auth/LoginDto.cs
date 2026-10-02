using System.ComponentModel.DataAnnotations;

namespace Auth.DTOs.Auth
{
    public class LoginDto
    {
        [Required]
        public string UserName { get; set; } = string.Empty;

        [Required]
        public string Password { get; set; } = string.Empty;

        // Optional parameter with "Email" as default
        public string Provider { get; set; } = "Email";
    }
}