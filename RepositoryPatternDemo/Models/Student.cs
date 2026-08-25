using System.ComponentModel.DataAnnotations;

namespace RepositoryPatternDemo.Models
{
    public class Student
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Student Name is required.")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid Email Address.")]
        public string Email { get; set; } = string.Empty;

        public int? DepartmentId { get; set; }
        public Department? Department { get; set; }
    }
}