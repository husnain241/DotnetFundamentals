using System.ComponentModel.DataAnnotations;

namespace UMS_Sp.DTOs
{
    public class CourseDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public int Credits { get; set; }
        public int DepartmentId { get; set; }
        public string? DepartmentName { get; set; }
    }

    public class CreateCourseDto
    {
        [Required]
        [MaxLength(150)]
        public string Title { get; set; } = string.Empty;

        [Required]
        [Range(1, 10)]
        public int Credits { get; set; }

        [Required]
        public int DepartmentId { get; set; }
    }

    public class UpdateCourseDto
    {
        [Required]
        [MaxLength(150)]
        public string Title { get; set; } = string.Empty;

        [Required]
        [Range(1, 10)]
        public int Credits { get; set; }

        [Required]
        public int DepartmentId { get; set; }
    }

    public class CourseWithStudentCountDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public int Credits { get; set; }
        public int DepartmentId { get; set; }
        public string DepartmentName { get; set; } = string.Empty;
        public int EnrolledStudentsCount { get; set; }
    }
}
