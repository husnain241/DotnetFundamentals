using MiniProject_StudentManagementSys.Seeders;
using MiniProject_StudentManagementSys.Services;
using MiniProject_StudentManagementSys.UI;
using MiniProject_StudentManagementSys.Interfaces;

IDepartmentService departmentService = new DepartmentService();
IStudentService studentService = new StudentService(departmentService);

StudentSeeder.Seed(departmentService, studentService);

var menu = new StudentMenu(studentService);
menu.Run();