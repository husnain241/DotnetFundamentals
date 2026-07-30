using Linq_Basics.Models;

namespace Linq_Basics.Services
{
    public class StudentService
    {
        private readonly List<Student> _students;

        public StudentService(List<Student> students)
        {
            _students = students;
        }


        public IEnumerable<Student> GetAllStudents()
        {
            return _students;
        }

       
        public IEnumerable<Student> GetTopStudents()
        {
            return _students.Where(student => student.Marks >= 90);
        }

      
        public IEnumerable<string> GetStudentNames()
        {
            return _students.Select(student => student.Name);
        }

        
        public IEnumerable<Student> GetStudentsByName()
        {
            return _students.OrderBy(student => student.Name);
        }

      
        public IEnumerable<Student> GetStudentsByMarks()
        {
            return _students.OrderByDescending(student => student.Marks);
        }

        
        public Student? GetStudentById(int id)
        {
            return _students.FirstOrDefault(student => student.Id == id);
        }
    }
}