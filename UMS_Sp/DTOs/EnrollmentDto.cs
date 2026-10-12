using System.ComponentModel.DataAnnotations;

namespace UMS_Sp.DTOs
{
    public class EnrollmentDto
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public int CourseId { get; set; }
        public DateTime EnrollmentDate { get; set; }
        public string? Grade { get; set; }
        public decimal? Marks { get; set; }
    }

    public class CreateEnrollmentDto
    {
        [Required]
        public int StudentId { get; set; }

        [Required]
        public int CourseId { get; set; }

        public DateTime? EnrollmentDate { get; set; }

        [RegularExpression("^[ABCDF]$", ErrorMessage = "Grade must be one of: A, B, C, D, F")]
        public string? Grade { get; set; }

        [Range(0, 100)]
        public decimal? Marks { get; set; }
    }

    public class UpdateEnrollmentDto
    {
        [RegularExpression("^[ABCDF]$", ErrorMessage = "Grade must be one of: A, B, C, D, F")]
        public string? Grade { get; set; }

        [Range(0, 100)]
        public decimal? Marks { get; set; }
    }

    public class EnrollmentDetailDto
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public string StudentName { get; set; } = string.Empty;
        public string StudentEmail { get; set; } = string.Empty;
        public int CourseId { get; set; }
        public string CourseTitle { get; set; } = string.Empty;
        public int CourseCredits { get; set; }
        public DateTime EnrollmentDate { get; set; }
        public string? Grade { get; set; }
        public decimal? Marks { get; set; }
    }
}
