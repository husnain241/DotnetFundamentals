using AspNetCoreFundamentals.Interface;
using AspNetCoreFundamentals.Models;
using System.Xml.Linq;

public class StudentService : IStudentService
{

    private readonly List<Student> _students = new List<Student>()
    {
          new Student
        {
            Id = 1,
            Name = "Ali",
            Age = 22,
            Email = "ali@example.com",
            IsActive = true
        },
        new Student
        {
            Id = 2,
            Name = "Ahmed",
            Age = 23,
            Email = "ahmed@example.com",
            IsActive = true
        }
    };
    public IEnumerable<Student> GetAll()
    {       
        return _students;
    }

    public Student? GetById(int id)
    {
        return _students.FirstOrDefault(x => x.Id == id);
    }

    public void Add(Student student)
    {
        if (_students.Any(x =>
            x.Email.Equals(student.Email, StringComparison.OrdinalIgnoreCase)))
        {
            throw new InvalidOperationException(
                "A student with this email already exists.");
        }

        student.Id = _students.Count + 1;

        _students.Add(student);
    }

    public void Update(Student student)
    {
        var existingStudent = GetById(student.Id);

        if (existingStudent == null)
        {
            throw new KeyNotFoundException("Student not found.");
        }

        existingStudent.Name = student.Name;
        existingStudent.Age = student.Age;
        existingStudent.Email = student.Email;
        existingStudent.IsActive = student.IsActive;
    }

    public void Delete(int id)
    {
        var student = GetById(id);

        if (student == null)
        {
            throw new KeyNotFoundException("Student not found.");
        }

        _students.Remove(student);
    }
}