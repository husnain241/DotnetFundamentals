namespace Interfaces_Abstraction.Helpers
{
    public static class ConsoleHelper
    {
        public static int ReadInt(string message)
        {
            while (true)
            {
                Console.Write(message);

                if (int.TryParse(Console.ReadLine(), out int value))
                {
                    return value;
                }

                Console.WriteLine("Invalid number. Please try again.");
            }
        }

        public static decimal ReadDecimal(string message)
        {
            while (true)
            {
                Console.Write(message);

                if (decimal.TryParse(Console.ReadLine(), out decimal value) && value > 0)
                {
                    return value;
                }

                Console.WriteLine("Please enter a valid amount greater than zero.");
            }
        }

        public static bool ReadBool(string message)
        {
            while (true)
            {
                Console.Write($"{message} (Y/N): ");

                string? input = Console.ReadLine();

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
        public static string ReadString(string message)
        {
            Console.Write(message);
            return Console.ReadLine() ?? string.Empty;
        }   

        public static void Pause()
        {
            Console.WriteLine();
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }
    }
}