using RepositoryPatternDemo.Models;
using RepositoryPatternDemo.Repositories.Interfaces;

namespace RepositoryPatternDemo.Repositories
{
    public class StudentRepository : IStudentRepository
    {
        private readonly List<Student> _students = new()
{
    new Student
    {
        Id = 1,
        Name = "Ali",
        Email = "ali@example.com"
    },
    new Student
    {
        Id = 2,
        Name = "Ahmed",
        Email = "ahmed@example.com"
    }
};

        public List<Student> GetAll()
        {
            return _students;
        }

        public Student? GetById(int id)
        {
            return _students.FirstOrDefault(s => s.Id == id);
        }

        public void Add(Student student)
        {
            _students.Add(student);
        }

        public void Update(Student student)
        {
            var existingStudent = GetById(student.Id);

            if (existingStudent != null)
            {
                existingStudent.Name = student.Name;
                existingStudent.Email = student.Email;
            }
        }

        public void Delete(int id)
        {
            var student = GetById(id);

            if (student != null)
            {
                _students.Remove(student);
            }
        }
    }
}