using System.ComponentModel.DataAnnotations;

namespace Auth.DTOs.Roles
{
    public class AssignRoleDto
    {
        [Required]
        public string UserId { get; set; } = string.Empty;

        [Required]
        public string RoleName { get; set; } = string.Empty;
    }
}