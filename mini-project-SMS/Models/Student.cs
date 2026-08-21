using System.ComponentModel.DataAnnotations;

namespace mini_project_SMS.Models
{

        public class Student
        {
            public int Id { get; set; }

            [Required(ErrorMessage = "Student name is required.")]
            [StringLength(50, ErrorMessage = "Name cannot exceed 50 characters.")]
            public string Name { get; set; } = string.Empty;

            [Required(ErrorMessage = "Email is required.")]
            [EmailAddress(ErrorMessage = "Please enter a valid email address.")]
            public string Email { get; set; } = string.Empty;

            [Range(16, 60, ErrorMessage = "Age must be between 16 and 60.")]
            public int Age { get; set; }

            [Required(ErrorMessage = "Department is required.")]
             public int DepartmentId { get; set; }

        public string? ImagePath { get; set; }
        }
    }
