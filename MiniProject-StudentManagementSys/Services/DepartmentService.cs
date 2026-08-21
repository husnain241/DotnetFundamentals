using MiniProject_StudentManagementSys.Models;
using MiniProject_StudentManagementSys.Interfaces;

namespace MiniProject_StudentManagementSys.Services;


public class DepartmentService : IDepartmentService    
{
    private readonly List<Department> _departments = new();

    public void AddDepartment(Department department) => _departments.Add(department);

    public IReadOnlyList<Department> GetAll() => _departments.AsReadOnly();

    public Department? GetById(int id) => _departments.FirstOrDefault(d => d.Id == id);

    public bool Exists(int id) => _departments.Any(d => d.Id == id);

    public string GetNameById(int id) => GetById(id)?.Name ?? "Unknown";
}
