using RepositoryPatternDemo.Data;
using RepositoryPatternDemo.Repositories.Interfaces;

namespace RepositoryPatternDemo.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;

        public IStudentRepository Students { get; }
        public IDepartmentRepository Departments { get; }

        public UnitOfWork(ApplicationDbContext context, IStudentRepository studentRepository, IDepartmentRepository departmentRepository)
        {
            _context = context;
            Students = studentRepository;
            Departments = departmentRepository;
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public int Complete()
        {
            return _context.SaveChanges();
        }
    }
}
