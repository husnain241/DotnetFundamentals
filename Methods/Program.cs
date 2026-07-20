Console.WriteLine("Grade Calculator By using methods");

while (true)
{
    int marks = ReadMarks();
    char grade = CalculateGrade(marks);

    Console.WriteLine($"The grade for marks {marks} is: {grade}");

    while (true)
    {
        Console.Write("\nDo you want to calculate grade again? (Y/N): ");
        char choice = char.ToUpper(Console.ReadKey().KeyChar);
        Console.WriteLine();

        switch (choice)
        {
            case 'Y':
                Console.Clear();
                break; // Exit the inner loop

            case 'N':
                Console.WriteLine("Press any key to exit...");
                Console.ReadKey();
                return;

            default:
                Console.WriteLine("Invalid. Please enter Y or N.");
                continue;
        }

        break; // Exit the inner loop and continue the outer loop
    }
}

// Method: Read student marks
static int ReadMarks()
{
    while (true)
    {
        Console.Write("Enter the mark: ");

        if (!int.TryParse(Console.ReadLine(), out int marks))
        {
            Console.WriteLine("Please enter a valid integer.");
            continue;
        }

        if (marks < 0 || marks > 100)
        {
            Console.WriteLine("Please enter marks between 0 and 100.");
            continue;
        }

        return marks;
    }
}

// Method: Calculate grade
static char CalculateGrade(int marks)
{
    if (marks >= 90) return 'A';
    if (marks >= 80) return 'B';
    if (marks >= 70) return 'C';
    if (marks >= 60) return 'D';

    return 'F';
}