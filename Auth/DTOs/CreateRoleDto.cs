using System.ComponentModel.DataAnnotations;

namespace Auth.DTOs
{
    public class CreateRoleDto
    {
        [Required]
        public string RoleName { get; set; } = string.Empty;
    }
}