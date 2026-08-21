using Advanced_Linq.DTOs;
using Advanced_Linq.Models;

namespace Advanced_Linq.Helpers
{
    public static class ConsoleHelper
    {
        public static int ReadInt(string message)
        {
            int value;

            while (true)
            {
                Console.Write(message);

                if (int.TryParse(Console.ReadLine(), out value))
                {
                    return value;
                }

                Console.WriteLine("Invalid input. Please enter a valid number.");
            }
        }

        public static void DisplayStudents(IEnumerable<StudentDepartmentDto> students)
        {
            if (!students.Any())
            {
                Console.WriteLine("No students found.");
                return;
            }

            foreach (var student in students)
            {
                DisplayStudent(student);
            }
        }
        public static void DisplayStudent(StudentDepartmentDto student)
        {
            Console.WriteLine($"ID         : {student.Id}");
            Console.WriteLine($"Name       : {student.Name}");
            Console.WriteLine($"Age        : {student.Age}");
            Console.WriteLine($"Department : {student.Department}");
            Console.WriteLine($"Marks      : {student.Marks}");
            Console.WriteLine(new string('-', 35));
        }

        public static void DisplayStudentNames(IEnumerable<string> studentNames)
        {
            if (!studentNames.Any())
            {
                Console.WriteLine("No students found.");
                return;
            }

            foreach (string name in studentNames)
            {
                Console.WriteLine(name);
            }
        }

        public static void DisplayStudentsByDepartment(IEnumerable<IGrouping<string, StudentDepartmentDto>> groupedStudents)
        {
            if (!groupedStudents.Any())
            {
                Console.WriteLine("No students found.");
                return;
            }
            foreach (var group in groupedStudents)
            {
                Console.WriteLine($"===Department===: {group.Key}");
                Console.WriteLine(new string('-', 35));
                DisplayStudents(group);
            }
        }

        public static void DisplayAverageMarksByDepartment(IEnumerable<DepartmentAverageDto> departmentAverages)
        {
            if (!departmentAverages.Any())
            {
                Console.WriteLine("No data found.");
                return;
            }

            Console.WriteLine($"{"Department",-20} {"Avg Marks",-12} {"Students"}");
            Console.WriteLine(new string('-', 45));

            foreach (var dept in departmentAverages)
            {
                Console.WriteLine($"{dept.Department,-20} {dept.AverageMarks,-12:F2} {dept.StudentCount}");
            }
        }

        public static void Pause()
        {
            Console.WriteLine();
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }
    }
}