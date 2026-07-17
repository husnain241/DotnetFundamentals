
Console.WriteLine("=== Guessing Game ===");

while (true)
{
    Random random = new Random();
    int secretNumber = random.Next(1, 101);
    int attempt = 0;

    while (true)
    {
        int userGuess = ReadGuess();
        attempt++;

        if (userGuess == secretNumber)
        {
            Console.WriteLine("Congratulations! You guessed the correct number.");
            break;
        }
        else if (userGuess < secretNumber)
        {
            Console.WriteLine("Too Low! Try again.");
        }
        else
        {
            Console.WriteLine("Too High! Try again.");
        }
    }

    Console.WriteLine($"You won the game in {attempt} attempts.");
    Console.WriteLine();

    while (true)
    {
        Console.Write("Do you want to play again? (Y/N): ");
        char choice = char.ToUpper(Console.ReadKey().KeyChar);
        Console.WriteLine();

        switch (choice)
        {
            case 'Y':
                Console.Clear();

                Console.WriteLine("=== Guessing Game ===");
                goto StartNewGame;

            case 'N':
                Console.WriteLine("Thanks for playing!");

                Console.WriteLine("Press any key to exit...");
                Console.ReadKey();
                return;

            default:
                Console.WriteLine("Invalid choice. Please enter Y or N.");
                break;
        }
    }

       StartNewGame:
    continue;
}

static int ReadGuess()
{
    while (true)
    {
        Console.Write("Enter your guess (1-100): ");

        if (!int.TryParse(Console.ReadLine(), out int userGuess))
        {
            Console.WriteLine("Invalid input. Please enter a valid number.");
            continue;
        }

        if (userGuess < 1 || userGuess > 100)
        {
            Console.WriteLine("Please enter a number between 1 and 100.");
            continue;
        }

        return userGuess;
    }
}