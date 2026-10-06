using Microsoft.EntityFrameworkCore;
using RepositoryPatternDemo.Data;
using RepositoryPatternDemo.Models;
using RepositoryPatternDemo.Repositories.Interfaces;

namespace RepositoryPatternDemo.Repositories
{
    public class StudentRepository : IStudentRepository
    {
        private readonly ApplicationDbContext _context;
        private static readonly Func<ApplicationDbContext, int, Task<Student?>> _getByIdCompiled =
           EF.CompileAsyncQuery((ApplicationDbContext context, int id) =>
               context.Students
                   .Include(s => s.Department) 
                   .FirstOrDefault(s => s.Id == id));
        private static readonly Func<ApplicationDbContext, int, IAsyncEnumerable<Student>> _getByDepartmentCompiled =
            EF.CompileAsyncQuery((ApplicationDbContext context, int departmentId) =>
                context.Students.Where(s => s.DepartmentId == departmentId));

        public StudentRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Student> GetAllAsNoTracking()
        {
            return _context.Students.AsNoTracking().ToList();
        }
        public IEnumerable<Student> GetAllWithDepartment()
        {
            return _context.Students.Include(s => s.Department).ToList();
        }
        public List<Student> GetAll()
        {
            return _context.Students.ToList();
        }

        public Student? GetById(int id)
        {
            return _context.Students.Find(id);
        }

        public async Task<Student?> GetByIdCompiledAsync(int id)
        {
            return await _getByIdCompiled(_context, id);
        }
        public async Task<List<Student>> GetByDepartmentCompiledAsync(int departmentId)
        {
            var students = new List<Student>();

            await foreach (var student in _getByDepartmentCompiled(_context, departmentId))
            {
                students.Add(student);
            }

            return students;
        }

        public void Add(Student student)
        {
            _context.Students.Add(student);
        }

        public void Update(Student student)
        {
            _context.Students.Update(student);
        }

        public void Delete(int id)
        {
            var student = GetById(id);

            if (student != null)
            {
                _context.Students.Remove(student);
            }
        }
    }
}