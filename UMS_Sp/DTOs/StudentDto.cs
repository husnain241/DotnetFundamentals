using System.ComponentModel.DataAnnotations;

namespace UMS_Sp.DTOs
{
    public class StudentDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public DateTime EnrollmentDate { get; set; }
        public int DepartmentId { get; set; }
    }

    public class CreateStudentDto
    {
        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        [MaxLength(150)]
        public string Email { get; set; } = string.Empty;

        public DateTime? EnrollmentDate { get; set; }

        [Required]
        public int DepartmentId { get; set; }
    }

    public class UpdateStudentDto
    {
        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        [MaxLength(150)]
        public string Email { get; set; } = string.Empty;

        [Required]
        public int DepartmentId { get; set; }
    }

    public class StudentDetailDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public DateTime EnrollmentDate { get; set; }
        public int DepartmentId { get; set; }
        public string DepartmentName { get; set; } = string.Empty;
        public string HeadOfDepartment { get; set; } = string.Empty;
    }

    public class EnrolledCourseDto
    {
        public int EnrollmentId { get; set; }
        public int CourseId { get; set; }
        public string CourseTitle { get; set; } = string.Empty;
        public int Credits { get; set; }
        public DateTime EnrollmentDate { get; set; }
        public string? Grade { get; set; }
        public decimal? Marks { get; set; }
    }

    public class StudentWithCoursesDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public DateTime EnrollmentDate { get; set; }
        public int DepartmentId { get; set; }
        public string DepartmentName { get; set; } = string.Empty;
        public List<EnrolledCourseDto> EnrolledCourses { get; set; } = new();
    }
}
