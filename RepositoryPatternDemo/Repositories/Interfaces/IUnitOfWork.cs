using RepositoryPatternDemo.Repositories.Interfaces;

namespace RepositoryPatternDemo.Repositories.Interfaces
{
    public interface IUnitOfWork
    {
        IStudentRepository Students { get; }
        IDepartmentRepository Departments { get; }
        int Complete();
        void BeginTransaction();
        void Commit();
        void Rollback();
    }
}
