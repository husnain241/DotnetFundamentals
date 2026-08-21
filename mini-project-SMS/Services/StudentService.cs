using mini_project_SMS.Models;
using mini_project_SMS.Services.Interfaces;
using System.Xml.Linq;

namespace mini_project_SMS.Services
{
    public class StudentService : IStudentService
    {
        private readonly List<Student> _students = new()
        {
            new Student
    {
        Id = 1,
        Name = "Ali",
        Email = "ali@example.com",
        Age = 22,
        DepartmentId = 1
    },
    new Student
    {
        Id = 2,
        Name = "Ahmed",
        Email = "ahmed@example.com",
        Age = 23,
        DepartmentId = 2
    }
        };

        public IEnumerable<Student> GetAll()
        {
            return _students;
        }

        public Student? GetById(int id)
        {
            return _students.FirstOrDefault(s => s.Id == id);
        }

        public void Add(Student student)
        {

            student.Id = _students.Count == 0
                   ? 1
                   : _students.Max(s => s.Id) + 1;

            _students.Add(student);
        }

        public bool Update(Student student)
        {
            var existingStudent = GetById(student.Id);

            if (existingStudent == null)
                return false;

            existingStudent.Name = student.Name;
            existingStudent.Email = student.Email;
            existingStudent.Age = student.Age;
            existingStudent.DepartmentId = student.DepartmentId;
            existingStudent.ImagePath = student.ImagePath;

            return true;
        }

        public bool Delete(int id)
        {
            var student = GetById(id);

            if (student == null)
                return false;

            _students.Remove(student);

            return true;
        }


        public IEnumerable<Student> Search(string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
            {
                return GetAll();
            }

            return _students.Where(s =>
                s.Name.Contains(searchTerm, StringComparison.OrdinalIgnoreCase) ||
                s.Email.Contains(searchTerm, StringComparison.OrdinalIgnoreCase)
            );
        }
    }
}
