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
        [Required(ErrorMessage = "Department Name is required.")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Department Name must be between 3 and 100 characters.")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Head of Department is required.")]
        [StringLength(100, ErrorMessage = "Head of Department name cannot exceed 100 characters.")]
        public string HeadOfDepartment { get; set; } = string.Empty;
    }

    public class UpdateDepartmentDto
    {
        [Required(ErrorMessage = "Department Name is required.")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Department Name must be between 3 and 100 characters.")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Head of Department is required.")]
        [StringLength(100, ErrorMessage = "Head of Department name cannot exceed 100 characters.")]
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
