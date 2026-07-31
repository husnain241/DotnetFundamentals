using Interfaces_Abstraction.UI;


namespace Interfaces_Abstraction;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Payment Management System");
        try
        {
            Console.WriteLine("Initializing Payment Service...");
            PaymentService paymentService = new PaymentService();
            Console.WriteLine("Payment Service initialized successfully.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error initializing Payment Service: {ex.Message}");
            return;
        }
        ConsoleUI consoleUI = new ConsoleUI();
        consoleUI.Run();
        Console.WriteLine("Press Enter to exit...");
        Console.ReadLine();
    }
};
