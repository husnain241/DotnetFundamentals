using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UMS_Sp.Models
{
    public class Enrollment
    {
        public int Id { get; set; }

        [ForeignKey(nameof(Student))]
        public int StudentId { get; set; }

        [ForeignKey(nameof(Course))]
        public int CourseId { get; set; }

        public DateTime EnrollmentDate { get; set; } = DateTime.UtcNow;

        [RegularExpression("^[ABCDF]$", ErrorMessage = "Grade must be one of: A, B, C, D, F")]
        [MaxLength(2)]
        public string? Grade { get; set; }

        [Range(0, 100)]
        public decimal? Marks { get; set; }

        // Navigation properties
        public Student? Student { get; set; }
        public Course? Course { get; set; }
    }
}
