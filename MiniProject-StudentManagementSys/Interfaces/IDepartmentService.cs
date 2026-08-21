using MiniProject_StudentManagementSys.Models;

namespace MiniProject_StudentManagementSys.Interfaces;

public interface IDepartmentService
{
	void AddDepartment(Department department);

	IReadOnlyList<Department> GetAll();

	Department? GetById(int id);

	bool Exists(int id);

	string GetNameById(int id);
}