using RepositoryPatternDemo.Models;
using RepositoryPatternDemo.Repositories.Interfaces;
using RepositoryPatternDemo.Services.Interfaces;

namespace RepositoryPatternDemo.Services
{
    public class DepartmentService : IDepartmentService
    {
        private readonly IDepartmentRepository _departmentRepository;

        public DepartmentService(IDepartmentRepository departmentRepository)
        {
            _departmentRepository = departmentRepository;
        }

        public List<Department> GetAll()
        {
            return _departmentRepository.GetAll();
        }

        public Department? GetById(int id)
        {
            return _departmentRepository.GetById(id);
        }

        public void Add(Department department)
        {
            _departmentRepository.Add(department);
        }

        public void Update(Department department)
        {
            _departmentRepository.Update(department);
        }

        public void Delete(int id)
        {
            _departmentRepository.Delete(id);
        }
    }
}