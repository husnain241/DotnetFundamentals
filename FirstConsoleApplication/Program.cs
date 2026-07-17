const string AppName = "Simple Calculator";

Console.WriteLine($"=== {AppName} ===");
Console.WriteLine();

double firstNumber = ReadNumber("Enter first number: ");

char arithmeticOperator = ReadOperator();

Console.WriteLine();

double secondNumber = ReadNumber("Enter second number: ");

Console.WriteLine();

double result = 0;

switch (arithmeticOperator)
{
    case '+':
        result = firstNumber + secondNumber;
        break;

    case '-':
        result = firstNumber - secondNumber;
        break;

    case '*':
        result = firstNumber * secondNumber;
        break;

    case '/':
        if (secondNumber != 0)
        {
            result = firstNumber / secondNumber;
        }
        else
        {
            Console.WriteLine("Cannot divide by zero.");
            return;
        }
        break;
}

int convertedResult = (int)result;

Console.WriteLine("Result");
Console.WriteLine("-------------------------");
Console.WriteLine($"Original Result : {result}");
Console.WriteLine($"Converted Result: {convertedResult}");

Console.WriteLine();
Console.WriteLine("Press any key to exit...");
Console.ReadKey();

static double ReadNumber(string message)
{
    double number;

    while (true)
    {
        Console.Write(message);

        if (double.TryParse(Console.ReadLine(), out number))
        {
            return number;
        }

        Console.WriteLine("Invalid input. Please enter a valid number.");
    }
}

static char ReadOperator()
{
    while (true)
    {
        Console.Write("Enter operator (+, -, *, /): ");

        char op = Console.ReadKey().KeyChar;
        Console.WriteLine();

        if (op == '+' || op == '-' || op == '*' || op == '/')
        {
            return op;
        }

        Console.WriteLine("Invalid operator. Please enter +, -, *, or /.");
    }
}