Console.WriteLine("=== Simple Calculator ===");
Console.WriteLine();

double firstNumber = ReadNumber("Enter first number: ");

Console.Write("Enter operator (+, -, *, /): ");
char arithmeticOperator = Console.ReadKey().KeyChar;

Console.WriteLine();
Console.WriteLine();

double secondNumber = ReadNumber("Enter second number: ");

Console.WriteLine();
Console.WriteLine("Result");
Console.WriteLine("-------------------------");

switch (arithmeticOperator)
{
    case '+':
        Console.WriteLine($"Result: {firstNumber + secondNumber}");
        break;

    case '-':
        Console.WriteLine($"Result: {firstNumber - secondNumber}");
        break;

    case '*':
        Console.WriteLine($"Result: {firstNumber * secondNumber}");
        break;

    case '/':
        if (secondNumber != 0)
        {
            Console.WriteLine($"Result: {firstNumber / secondNumber}");
        }
        else
        {
            Console.WriteLine("Cannot divide by zero.");
        }
        break;

    default:
        Console.WriteLine("Invalid operator.");
        break;
}

static double ReadNumber(string message)
{
    Console.Write(message);

    while (!double.TryParse(Console.ReadLine(), out double number))
    {
        Console.Write("Invalid input. Please enter a valid number: ");
    }

    return number;
}