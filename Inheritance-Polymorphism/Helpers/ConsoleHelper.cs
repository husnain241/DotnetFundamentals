using Inheritance_Polymorphism.Enums;

namespace Inheritance_Polymorphism.Helpers
{
	public static class ConsoleHelper
	{
		public static string? ReadString(
			string message,
			InputMode mode = InputMode.Required)
		{
			while (true)
			{
				Console.Write(message);

				string? value = Console.ReadLine();

				if (string.IsNullOrWhiteSpace(value))
				{
					if (mode == InputMode.Optional)
					{
						return null;
					}

					Console.WriteLine("Value cannot be empty.");
					continue;
				}

				return value.Trim();
			}
		}

		public static int? ReadInt(
			string message,
			InputMode mode = InputMode.Required)
		{
			while (true)
			{
				Console.Write(message);

				string? input = Console.ReadLine();

				if (string.IsNullOrWhiteSpace(input))
				{
					if (mode == InputMode.Optional)
					{
						return null;
					}

					Console.WriteLine("Value cannot be empty.");
					continue;
				}

				if (int.TryParse(input, out int value))
				{
					return value;
				}

				Console.WriteLine("Invalid number. Please try again.");
			}
		}

		public static double? ReadDouble(
			string message,
			InputMode mode = InputMode.Required)
		{
			while (true)
			{
				Console.Write(message);

				string? input = Console.ReadLine();

				if (string.IsNullOrWhiteSpace(input))
				{
					if (mode == InputMode.Optional)
					{
						return null;
					}

					Console.WriteLine("Value cannot be empty.");
					continue;
				}

				if (double.TryParse(input, out double value))
				{
					return value;
				}

				Console.WriteLine("Invalid number. Please try again.");
			}
		}

		public static bool? ReadBool(
			string message,
			InputMode mode = InputMode.Required)
		{
			while (true)
			{
				Console.Write(message);

				string? input = Console.ReadLine();

				if (string.IsNullOrWhiteSpace(input))
				{
					if (mode == InputMode.Optional)
					{
						return null;
					}

					Console.WriteLine("Value cannot be empty.");
					continue;
				}

				if (string.Equals(input, "Y", StringComparison.OrdinalIgnoreCase))
				{
					return true;
				}

				if (string.Equals(input, "N", StringComparison.OrdinalIgnoreCase))
				{
					return false;
				}

				Console.WriteLine("Please enter Y or N.");
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