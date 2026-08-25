using RepositoryPatternDemo.Repositories.Interfaces;

namespace RepositoryPatternDemo.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        public IStudentRepository Students { get; }
        public IDepartmentRepository Departments { get; }

        public UnitOfWork(IStudentRepository studentRepository, IDepartmentRepository departmentRepository)
        {
            Students = studentRepository;
            Departments = departmentRepository;
        }

        public void Save()
        {
            // Simple in-memory Save implementation
        }
    }
}
