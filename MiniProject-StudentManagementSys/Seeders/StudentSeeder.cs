using MiniProject_StudentManagementSys.Models;
using MiniProject_StudentManagementSys.Services;
using MiniProject_StudentManagementSys.Interfaces;

namespace MiniProject_StudentManagementSys.Seeders;


public static class StudentSeeder
{
    public static void Seed(IDepartmentService departmentService, IStudentService studentService)
    {
        departmentService.AddDepartment(new Department(1, "Computer Science"));
        departmentService.AddDepartment(new Department(2, "Electrical Engineering"));
        departmentService.AddDepartment(new Department(3, "Business Administration"));
        departmentService.AddDepartment(new Department(4, "Mechanical Engineering"));
        departmentService.AddDepartment(new Department(5, "Civil Engineering"));

        studentService.AddStudent(new Student(1, "Ali Raza", 67.5, 1));
        studentService.AddStudent(new Student(2, "Sara Khan", 92.0, 1));
        studentService.AddStudent(new Student(3, "Bilal Ahmed", 76.25, 2));
        studentService.AddStudent(new Student(4, "Ayesha Tariq", 65.0, 3));
        studentService.AddStudent(new Student(5, "Hamza Sheikh", 99.0, 2));
        studentService.AddStudent(new Student(6, "Zara Malik", 85.0, 3));
        studentService.AddStudent(new Student(7, "Omar Farooq", 88.0, 4));
        studentService.AddStudent(new Student(8, "Aisha Javaid", 91.5, 5));
        studentService.AddStudent(new Student(9, "Hassan Rafiq", 79.0, 1));
        studentService.AddStudent(new Student(10, "Fatima Zahra", 82.0, 2));
        studentService.AddStudent(new Student(11, "Yusuf Ali", 87.5, 4));
        studentService.AddStudent(new Student(12, "Maria Jose", 90.0, 5));

    }
}
