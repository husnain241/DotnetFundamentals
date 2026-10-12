using UMS_Sp.Data;

namespace UMS_Sp.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        private IDepartmentRepository? _departments;
        private IStudentRepository? _students;
        private ICourseRepository? _courses;
        private IEnrollmentRepository? _enrollments;
        private bool _disposed = false;

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
        }

        public IDepartmentRepository Departments => _departments ??= new DepartmentRepository(_context);
        public IStudentRepository Students => _students ??= new StudentRepository(_context);
        public ICourseRepository Courses => _courses ??= new CourseRepository(_context);
        public IEnrollmentRepository Enrollments => _enrollments ??= new EnrollmentRepository(_context);

        public async Task<int> CompleteAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
                _disposed = true;
            }
        }
    }
}
