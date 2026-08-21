using mini_project_SMS.Models;

namespace mini_project_SMS.Services.Interfaces
{
    public interface IDepartmentService
    {
        IEnumerable<Department> GetAll();

        Department? GetById(int id);

        void Add(Department department);
    }
}
