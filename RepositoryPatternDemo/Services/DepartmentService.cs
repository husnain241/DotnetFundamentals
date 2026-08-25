using RepositoryPatternDemo.Models;
using RepositoryPatternDemo.Repositories.Interfaces;
using RepositoryPatternDemo.Services.Interfaces;

namespace RepositoryPatternDemo.Services
{
    public class DepartmentService : IDepartmentService
    {
        private readonly IUnitOfWork _unitOfWork;

        public DepartmentService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public List<Department> GetAll()
        {
            return _unitOfWork.Departments.GetAll();
        }

        public Department? GetById(int id)
        {
            return _unitOfWork.Departments.GetById(id);
        }

        public void Add(Department department)
        {
            _unitOfWork.Departments.Add(department);
            _unitOfWork.Save();
        }

        public void Update(Department department)
        {
            _unitOfWork.Departments.Update(department);
            _unitOfWork.Save();
        }

        public void Delete(int id)
        {
            _unitOfWork.Departments.Delete(id);
            _unitOfWork.Save();
        }
    }
}