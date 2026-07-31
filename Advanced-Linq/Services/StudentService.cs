using Advanced_Linq.DTOs;
using Advanced_Linq.Models;

namespace Advanced_Linq.Services
{
    public class StudentService
    {
        private const double PassingMarks = 50;
        private const int ComputerScienceDepartmentId = 1;

        private readonly Enumerable<Student> _students;
        private readonly Enumerable<Department> _departments;

        public StudentService(Enumerable<Student> students, Enumerable<Department> departments)
        {
            _students = students ?? throw new ArgumentNullException(nameof(students));
            _departments = departments ?? throw new ArgumentNullException(nameof(departments));
        }

        public bool HasPassedStudents()
        {
            return _students.Any(s => s.Marks >= PassingMarks);
        } 


        public bool AreAllStudentsPassed()
        {
            if (!_students.Any())
                return false; 

            return _students.All(s => s.Marks >= PassingMarks);
        }

        public int GetStudentCount()
        {
            return _students.Count;
        }

        public int GetPassedStudentCount()
        {
            return _students.Count(s => s.Marks >= PassingMarks);
        }

        public double GetTotalMarks()
        {
            return _students.Sum(s => s.Marks);
        }

        public int GetComputerScienceStudentCount()
        {
            return _students.Count(s => s.DepartmentId == ComputerScienceDepartmentId);
        }

        public IEnumerable<IGrouping<string, StudentDepartmentDto>> GetStudentsByDepartment()
        {
            try
            {
                return _students
                    .Join(
                        _departments,
                        student => student.DepartmentId,
                        department => department.Id,
                        (student, department) => new StudentDepartmentDto
                        {
                            Id = student.Id,
                            Name = student.Name,
                            Age = student.Age,
                            Marks = student.Marks,
                            Department = department.Name
                        })
                    .GroupBy(student => student.Department)
                    .OrderBy(group => group.Key)
                    .ToList(); 
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error while grouping students by department: {ex.Message}");
                return Enumerable.Empty<IGrouping<string, StudentDepartmentDto>>();
            }
        }

        public IEnumerable<DepartmentAverageDto> GetAverageMarksByDepartment()
        {
            try
            {
                return _students
                    .Join(
                        _departments,
                        student => student.DepartmentId,
                        department => department.Id,
                        (student, department) => new { student, department })
                    .GroupBy(x => x.department.Name)
                    .Select(group => new DepartmentAverageDto
                    {
                        Department = group.Key,
                        AverageMarks = group.Average(x => x.student.Marks),
                        StudentCount = group.Count()
                    })
                    .OrderByDescending(d => d.AverageMarks)
                    .ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error while calculating average marks by department: {ex.Message}");
                return Enumerable.Empty<DepartmentAverageDto>();
            }
        }
    }
}