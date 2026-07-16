Console.WriteLine("=== Simple Calculator ===");
Console.WriteLine();

double ReadNumber(string message)
{
    Console.WriteLine(message);
    while (!double.TryParse(Console.ReadLine(), out double number) {
        Console.Write("Invalid input. please enter a valid number: ");
    }
    return number;
}
    double firstNumber = ReadNumber("Enter first number: ");
    double secondNumber = ReadNumber("Enter second number: ");

    Console.WriteLine();
Console.WriteLine("Results");
Console.WriteLine("-------------------------");
Console.WriteLine($"Addition       : {firstNumber + secondNumber}");
Console.WriteLine($"Subtraction    : {firstNumber - secondNumber}");
Console.WriteLine($"Multiplication : {firstNumber * secondNumber}");

if (secondNumber != 0)
{
    Console.WriteLine($"Division       : {firstNumber / secondNumber}");
}
else
{
    Console.WriteLine("Division       : Cannot divide by zero.");
}
Console.WriteLine("\nPress any key to exit...");
Console.ReadKey();