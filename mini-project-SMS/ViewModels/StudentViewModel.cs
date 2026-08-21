using Microsoft.AspNetCore.Http;
using mini_project_SMS.Models;

namespace mini_project_SMS.ViewModels
{
    public class StudentViewModel
    {
        public Student Student { get; set; } = new();

        public IEnumerable<Department> Departments { get; set; }
            = new List<Department>();

        public IFormFile? Image { get; set; }
    }
}