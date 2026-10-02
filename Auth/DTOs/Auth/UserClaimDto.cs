using System.ComponentModel.DataAnnotations;

namespace Auth.DTOs.Auth
{
    public class UserClaimDto
    {
        [Required]
        public string UserId { get; set; } = string.Empty;

        [Required]
        public string ClaimType { get; set; } = string.Empty;

        [Required]
        public string ClaimValue { get; set; } = string.Empty;
    }
}