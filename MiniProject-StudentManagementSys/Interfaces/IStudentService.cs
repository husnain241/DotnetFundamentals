using MiniProject_StudentManagementSys.DTOs;
using MiniProject_StudentManagementSys.Models;

namespace MiniProject_StudentManagementSys.Interfaces;

public interface IStudentService
{
    // CRUD
    bool IsIdTaken(int id);
    bool DepartmentExists(int departmentId);
    void AddStudent(Student student);
    IReadOnlyList<Student> GetAll();
    Student? GetById(int id);
    bool UpdateStudent(int id, string name, double marks, int departmentId);
    bool DeleteStudent(int id);

    // LINQ
    IReadOnlyList<Student> GetTopStudents(int count);
    IReadOnlyList<Student> GetStudentsByDepartment(int departmentId);
    IReadOnlyList<Student> GetStudentsAboveMarks(double minMarks);
    IReadOnlyList<string> GetStudentNames();
    double GetAverageMarks();
    int GetTotalStudents();
    IReadOnlyList<Student> SearchByName(string term);

    Dictionary<string, List<StudentDepartmentDto>> GroupByDepartment();
    IReadOnlyList<StudentDepartmentDto> GetAllWithDepartmentNames();

    // Statistics
    double? GetHighestMarks();
    double? GetLowestMarks();
    Dictionary<string, int> GetDepartmentWiseCount();
}