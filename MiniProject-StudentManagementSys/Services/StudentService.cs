using MiniProject_StudentManagementSys.DTOs;
using MiniProject_StudentManagementSys.Interfaces;
using MiniProject_StudentManagementSys.Models;

namespace MiniProject_StudentManagementSys.Services;

public class StudentService : IStudentService
{
    private readonly List<Student> _students = new();
    private readonly IDepartmentService _departmentService;

    public StudentService(IDepartmentService departmentService)
    {
        _departmentService = departmentService;
    }

    // ---------- CRUD ----------

    public bool IsIdTaken(int id) => _students.Any(s => s.Id == id);

    public bool DepartmentExists(int departmentId)
        => _departmentService.Exists(departmentId);

    public void AddStudent(Student student)
        => _students.Add(student);

    public IReadOnlyList<Student> GetAll()
        => _students.AsReadOnly();

    public Student? GetById(int id)
        => _students.FirstOrDefault(s => s.Id == id);

    public bool UpdateStudent(int id, string name, double marks, int departmentId)
    {
      

        student.UpdateName(name);
        student.UpdateMarks(marks);
        student.UpdateDepartment(departmentId);

        return true;
    }

    public bool DeleteStudent(int id)
    {
        var student = GetById(id);

        if (student is null)
            return false;

        _students.Remove(student);
        return true;
    }

    // ---------- LINQ ----------

    public IReadOnlyList<Student> GetTopStudents(int count)
        => _students
            .OrderByDescending(s => s.Marks)
            .Take(count)
            .ToList();

    public IReadOnlyList<Student> GetStudentsByDepartment(int departmentId)
        => _students
            .Where(s => s.DepartmentId == departmentId)
            .ToList();

    public IReadOnlyList<Student> GetStudentsAboveMarks(double minMarks)
        => _students
            .Where(s => s.Marks > minMarks)
            .ToList();

    public IReadOnlyList<string> GetStudentNames()
        => _students
            .Select(s => s.Name)
            .ToList();

    public double GetAverageMarks()
        => _students.Any()
            ? _students.Average(s => s.Marks)
            : 0;

    public int GetTotalStudents()
        => _students.Count;

    public IReadOnlyList<Student> SearchByName(string term)
        => _students
            .Where(s => s.Name.Contains(term, StringComparison.OrdinalIgnoreCase))
            .ToList();

    public Dictionary<string, List<StudentDepartmentDto>> GroupByDepartment()
        => _students
            .Join(
                _departmentService.GetAll(),
                student => student.DepartmentId,
                department => department.Id,
                (student, department) => new StudentDepartmentDto
                {
                    StudentId = student.Id,
                    StudentName = student.Name,
                    Marks = student.Marks,
                    DepartmentName = department.Name
                })
            .GroupBy(dto => dto.DepartmentName)
            .ToDictionary(
                g => g.Key,
                g => g.ToList());

    public IReadOnlyList<StudentDepartmentDto> GetAllWithDepartmentNames()
        => _students
            .Join(
                _departmentService.GetAll(),
                student => student.DepartmentId,
                department => department.Id,
                (student, department) => new StudentDepartmentDto
                {
                    StudentId = student.Id,
                    StudentName = student.Name,
                    Marks = student.Marks,
                    DepartmentName = department.Name
                })
            .ToList();

    // ---------- Statistics ----------

    public double? GetHighestMarks()
        => _students.Any()
            ? _students.Max(s => s.Marks)
            : null;

    public double? GetLowestMarks()
        => _students.Any()
            ? _students.Min(s => s.Marks)
            : null;

    public Dictionary<string, int> GetDepartmentWiseCount()
        => _students
            .Join(
                _departmentService.GetAll(),
                student => student.DepartmentId,
                department => department.Id,
                (student, department) => department.Name)
            .GroupBy(departmentName => departmentName)
            .ToDictionary(
                g => g.Key,
                g => g.Count());
}