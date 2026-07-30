using Linq_Basics.Models;

namespace Linq_Basics.Helpers
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

		public static void DisplayStudents(IEnumerable<Student> students)
		{
			if (!students.Any())
			{
				Console.WriteLine("No students found.");
				return;
			}

			foreach (Student student in students)
			{
				DisplayStudent(student);
			}
		}

		public static void DisplayStudent(Student student)
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

		public static void Pause()
		{
			Console.WriteLine();
			Console.WriteLine("Press any key to continue...");
			Console.ReadKey();
		}
	}
}