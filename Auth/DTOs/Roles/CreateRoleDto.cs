using System.ComponentModel.DataAnnotations;

namespace Auth.DTOs.Roles
{
    public class CreateRoleDto
    {
        [Required]
        public string RoleName { get; set; } = string.Empty;
    }
}