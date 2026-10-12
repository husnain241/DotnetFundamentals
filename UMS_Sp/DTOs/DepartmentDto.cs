using System.ComponentModel.DataAnnotations;

namespace UMS_Sp.DTOs
{
    public class DepartmentDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string HeadOfDepartment { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
    }

    public class CreateDepartmentDto
    {
        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = string.Empty;

        [Required]
        [MaxLength(100)]
        public string HeadOfDepartment { get; set; } = string.Empty;
    }

    public class UpdateDepartmentDto
    {
        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = string.Empty;

        [Required]
        [MaxLength(100)]
        public string HeadOfDepartment { get; set; } = string.Empty;
    }

    public class DepartmentWithStudentCountDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string HeadOfDepartment { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public int TotalStudents { get; set; }
    }
}
