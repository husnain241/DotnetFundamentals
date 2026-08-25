using RepositoryPatternDemo.Models;
using RepositoryPatternDemo.Repositories.Interfaces;

namespace RepositoryPatternDemo.Repositories
{
    public class DepartmentRepository : IDepartmentRepository
    {
        private readonly List<Department> _departments = new()
        {
            new Department
            {
                Id = 1,
                Name = "Computer Science"
            },
            new Department
            {
                Id = 2,
                Name = "Software Engineering"
            }
        };

        public List<Department> GetAll()
        {
            return _departments;
        }

        public Department? GetById(int id)
        {
            return _departments.FirstOrDefault(d => d.Id == id);
        }

        public void Add(Department department)
        {
            _departments.Add(department);
        }

        public void Update(Department department)
        {
            var existingDepartment = GetById(department.Id);

            if (existingDepartment != null)
            {
                existingDepartment.Name = department.Name;
            }
        }

        public void Delete(int id)
        {
            var department = GetById(id);

            if (department != null)
            {
                _departments.Remove(department);
            }
        }
    }
}