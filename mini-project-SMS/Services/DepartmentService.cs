using mini_project_SMS.Models;
using mini_project_SMS.Services.Interfaces;

namespace mini_project_SMS.Services
{
    public class DepartmentService : IDepartmentService
    {
        private readonly List<Department> _departments = new()
        {
            new Department { Id = 1, Name = "Computer Science" },
            new Department { Id = 2, Name = "Software Engineering" },
            new Department { Id = 3, Name = "Information Technology" }
        };

        public IEnumerable<Department> GetAll()
        {
            return _departments;
        }

        public Department? GetById(int id)
        {
            return _departments.FirstOrDefault(d => d.Id == id);
        }

        public void Add(Department department)
        {
            department.Id = _departments.Count == 0
                ? 1
                : _departments.Max(d => d.Id) + 1;

            _departments.Add(department);
        }
    }
}