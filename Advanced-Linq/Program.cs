using Advanced_Linq.Helpers;
using Advanced_Linq.Models;
using Advanced_Linq.Seed;
using Advanced_Linq.Services;

List<Student> students = StudentSeeder.GetStudents();
List<Department> departments = DepartmentSeeder.GetDepartments();

StudentService studentService = new StudentService(students, departments);

bool exit = false;



while (!exit)
{
    Console.Clear();

    Console.WriteLine("=========================================");
    Console.WriteLine("      Advanced LINQ Operations");
    Console.WriteLine("=========================================");
    Console.WriteLine("1. Check if Any Student has Passed");
    Console.WriteLine("2. Check if All Students have Passed");
    Console.WriteLine("3. Display Total Students");
    Console.WriteLine("4. Display Passed Students Count");
    Console.WriteLine("5. Display Computer Science Students Count");
    Console.WriteLine("6. Display Total Marks");
    Console.WriteLine("7. Display Students by Department");
    Console.WriteLine("8. Display Average Marks by Department");

    Console.WriteLine("0. Exit");
    Console.WriteLine("=========================================");

    int choice = ConsoleHelper.ReadInt("Enter your choice: ");

    Console.Clear();


    try
    {
        switch (choice)
        {
            case 1:

                Console.WriteLine(
                    studentService.HasPassedStudents()
                        ? "Yes, there is at least one student who has passed."
                        : "No student has passed.");

                break;

            case 2:

                Console.WriteLine(
                    studentService.AreAllStudentsPassed()
                        ? "All students have passed."
                        : "Some students have failed.");

                break;

            case 3:

                Console.WriteLine($"Total Students : {studentService.GetStudentCount()}");

                break;

            case 4:

                Console.WriteLine($"Passed Students : {studentService.GetPassedStudentCount()}");

                break;

            case 5:

                Console.WriteLine($"Computer Science Students : {studentService.GetComputerScienceStudentCount()}");

                break;

            case 6:

                Console.WriteLine($"Total Marks : {studentService.GetTotalMarks()}");

                break;

            case 7:

                var groupedStudents = studentService.GetStudentsByDepartment();

                ConsoleHelper.DisplayStudentsByDepartment(groupedStudents);

                break;

            case 8:

                var departmentAverages = studentService.GetAverageMarksByDepartment();

                ConsoleHelper.DisplayAverageMarksByDepartment(departmentAverages);

                break;


            case 0:

                exit = true;
                Console.WriteLine("Thank you for using Student Management System.");

                break;

            default:

                Console.WriteLine("Invalid choice. Please try again.");

                break;
        }

    }
    catch (Exception ex)
    {
        Console.WriteLine($"An unexpected error occurred: {ex.Message}");
    }

    if (!exit)
    {
        ConsoleHelper.Pause();
    }
}