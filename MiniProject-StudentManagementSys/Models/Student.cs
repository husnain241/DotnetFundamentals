namespace MiniProject_StudentManagementSys.Models;


public class Student
{
    public int Id { get; private set; }
    public string Name { get; private set; }
    public double Marks { get; private set; }
    public int DepartmentId { get; private set; }

    public Student(int id, string name, double marks, int departmentId)
    {
        Id = id;
        Name = name;
        Marks = marks;
        DepartmentId = departmentId;
    }

    public void UpdateName(string name) => Name = name;

    public void UpdateMarks(double marks) => Marks = marks;

    public void UpdateDepartment(int departmentId) => DepartmentId = departmentId;

    public override string ToString()
        => $"[{Id}] {Name} - Marks: {Marks:F2} - DeptId: {DepartmentId}";
}
