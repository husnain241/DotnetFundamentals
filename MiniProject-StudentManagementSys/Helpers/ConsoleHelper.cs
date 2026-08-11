using MiniProject_StudentManagementSys.Constants;

namespace MiniProject_StudentManagementSys.Helpers;


public static class ConsoleHelper
{
    public static int ReadInt(string prompt)
    {
        while (true)
        {
            Console.Write(prompt);
            var input = Console.ReadLine();

            if (int.TryParse(input, out var value))
                return value;

            PrintError(Messages.InvalidNumberInput);
        }
    }

    public static double ReadDouble(string prompt)
    {
        while (true)
        {
            Console.Write(prompt);
            var input = Console.ReadLine();

            if (double.TryParse(input, out var value))
                return value;

            PrintError(Messages.InvalidNumberInput);
        }
    }

    public static string ReadNonEmptyString(string prompt)
    {
        while (true)
        {
            Console.Write(prompt);
            var input = Console.ReadLine();

            if (ValidationHelper.IsNameValid(input))
                return input!.Trim();

            PrintError(Messages.EmptyName);
        }
    }

    public static void PrintError(string message)
    {
        var originalColor = Console.ForegroundColor;
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine(message);
        Console.ForegroundColor = originalColor;
    }

    public static void PrintSuccess(string message)
    {
        var originalColor = Console.ForegroundColor;
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine(message);
        Console.ForegroundColor = originalColor;
    }

    public static void PrintHeader(string header)
    {
        Console.WriteLine();
        Console.WriteLine(header);
    }

    public static void Pause()
    {
        Console.Write(Messages.PressEnterToContinue);
        Console.ReadLine();
    }
}
