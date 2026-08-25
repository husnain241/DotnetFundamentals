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
            // In-memory Save implementation
        }

        public int Complete()
        {
            Save();
            return 1;
        }
    }
}
