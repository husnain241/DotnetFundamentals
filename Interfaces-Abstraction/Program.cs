using Interfaces_Abstraction.UI;


namespace Interfaces_Abstraction;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Payment Management System");
        ConsoleUI consoleUI = new ConsoleUI();
        consoleUI.Run();
        Console.WriteLine("Press Enter to exit...");
        Console.ReadLine();
    }
};
