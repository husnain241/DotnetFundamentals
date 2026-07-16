Console.WriteLine("=== Simple Calculator ===");
Console.WriteLine();

Console.Write("Enter first number: ");
if (!double.TryParse(Console.ReadLine(), out double firstNumber))
{
    Console.WriteLine("Invalid first number.");
    return;
}

Console.Write("Enter second number: ");
if (!double.TryParse(Console.ReadLine(), out double secondNumber))
{
    Console.WriteLine("Invalid second number.");
    return;
}

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