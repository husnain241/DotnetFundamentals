using RepositoryPatternDemo.Repositories.Interfaces;

namespace RepositoryPatternDemo.Repositories.Interfaces
{
    public interface IUnitOfWork
    {
        IStudentRepository Students { get; }
        IDepartmentRepository Departments { get; }
        void Save();
        int Complete();
    }
}
