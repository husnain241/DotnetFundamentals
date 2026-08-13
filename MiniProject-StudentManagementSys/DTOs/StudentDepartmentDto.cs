namespace MiniProject_StudentManagementSys.DTOs;


public class StudentDepartmentDto
{
    public int StudentId { get; init; }
    public string StudentName { get; init; } = string.Empty;
    public double Marks { get; init; }
    public string DepartmentName { get; init; } = string.Empty;

    public override string ToString()
        => $"[{StudentId}] {StudentName} - Marks: {Marks:F2} - Dept: {DepartmentName}";
}
