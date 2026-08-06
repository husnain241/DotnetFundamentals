using Linq_Basics.Helpers;
using Linq_Basics.Models;
using Linq_Basics.Seed;
using Linq_Basics.Services;

List<Student> students = StudentSeeder.GetStudents();
StudentService studentService = new StudentService(students);

bool exit = false;

while (!exit)
{
    Console.Clear();

    Console.WriteLine("=====================================");
    Console.WriteLine("     Student Management System");
    Console.WriteLine("=====================================");
    Console.WriteLine("1. View All Students");
    Console.WriteLine("2. View Top Students (Marks >= 90)");
    Console.WriteLine("3. View Student Names");
    Console.WriteLine("4. Sort Students by Name");
    Console.WriteLine("5. Sort Students by Marks (Highest First)");
    Console.WriteLine("6. Search Student by ID");
    Console.WriteLine("0. Exit");
    Console.WriteLine("=====================================");

    int choice = ConsoleHelper.ReadInt("Enter your choice: ");

    Console.Clear();

    switch (choice)
    {
        case 1:
            Console.WriteLine("===== All Students =====");
            ConsoleHelper.DisplayStudents(studentService.GetAllStudents());
            break;

        case 2:
            Console.WriteLine("===== Top Students =====");
            ConsoleHelper.DisplayStudents(studentService.GetTopStudents());
            break;

        case 3:
            Console.WriteLine("===== Student Names =====");
            ConsoleHelper.DisplayStudentNames(studentService.GetStudentNames());
            break;

        case 4:
            Console.WriteLine("===== Students Sorted By Name =====");
            ConsoleHelper.DisplayStudents(studentService.GetStudentsByName());
            break;

        case 5:
            Console.WriteLine("===== Students Sorted By Marks =====");
            ConsoleHelper.DisplayStudents(studentService.GetStudentsByMarks());
            break;

        case 6:
            int id = ConsoleHelper.ReadInt("Enter Student ID: ");

            Student? student = studentService.GetStudentById(id);

            if (student != null)
            {
                Console.WriteLine();
                Console.WriteLine("===== Student Found =====");
                ConsoleHelper.DisplayStudent(student);
            }
            else
            {
                Console.WriteLine();
                Console.WriteLine("Student not found.");
            }
            break;

        case 0:
            exit = true;
            Console.WriteLine("Thank you for using Student Management System.");
            break;

        default:
            Console.WriteLine("Invalid choice. Please try again.");
            break;
    }

    if (!exit)
    {
        ConsoleHelper.Pause();
    }
}