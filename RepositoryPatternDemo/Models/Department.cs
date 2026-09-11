using System.ComponentModel.DataAnnotations;

namespace RepositoryPatternDemo.Models
{
    public class Department
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Department Name is required.")]
        public string Name { get; set; } = string.Empty;

        public int StudentCount { get; set; }

        public List<Student> Students { get; set; } = new();
    }
}