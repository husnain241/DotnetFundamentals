using RepositoryPatternDemo.Models;

namespace RepositoryPatternDemo.Services.Interfaces
{
    public interface IDepartmentService
    {
        List<Department> GetAll();
        Department? GetById(int id);
        void Add(Department department);
        void Update(Department department);
        void Delete(int id);
    }
}