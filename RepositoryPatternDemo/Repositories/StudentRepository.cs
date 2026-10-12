using Microsoft.EntityFrameworkCore;
using RepositoryPatternDemo.Data;
using RepositoryPatternDemo.Models;
using RepositoryPatternDemo.Repositories.Interfaces;

namespace RepositoryPatternDemo.Repositories
{
    public class StudentRepository : IStudentRepository
    {
        private readonly ApplicationDbContext _context;

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
            return _context.Students.FirstOrDefault(s => s.Id == id);
        }

        public void Add(Student student)
        {
            _context.Students.Add(student);
        }

        public void Update(Student student)
        {
            var existingStudent = _context.Students
                .FirstOrDefault(s => s.Id == student.Id);

            if (existingStudent != null)
            {
                existingStudent.Name = student.Name;
                existingStudent.Email = student.Email;
                existingStudent.DepartmentId = student.DepartmentId;
            }
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