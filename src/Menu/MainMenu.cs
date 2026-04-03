using System;
using System.Linq;

public static class MainMenu
{
    public static Random rand = new Random();

    static void Main()
    {
        (string option, Action action)[] options =
        {
            ("Exit", () => Environment.Exit(0)),
            ("Solves", Solves.Home),
            ("Do Solve", Solves.Do),
            ("Stats", Stats.Home)
        };

        Saving.CreateFiles();
        while (true)
        {
            Console.Clear();
            Console.WriteLine("S-Cube \nHOME\n");

            for (int i = 0; i < options.Length; i++)
            {
                Console.WriteLine($"{i}. {options[i].option}");
            }

            string input = GetNumber(false, options.Length - 1);

            //Handling Input
            if (input.StartsWith("Error"))
            {
                continue;
            }

            options[Convert.ToInt32(input)].action();
        }
    }

    public static void PrintError(string title, string message)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine($"Error: {title}");
        Console.WriteLine(message);
        Console.WriteLine("(Enter any key to continue)");
        Console.ReadKey();
        Console.ResetColor();
        Console.Clear();
    }

    public static string GetNumber(bool negativeAllowed, int? maxNumber)
    {
        Console.WriteLine("Enter a number");
        Console.Write("> ");
        string? input = Console.ReadLine().Trim();

        //---Error Handling
        //Reason for the error is included in return because it might be important

        if (string.IsNullOrWhiteSpace(input))
        {
            PrintError("Invalid Input", "Please enter something");
            return "Error: Empty Input";
        }

        if (!int.TryParse(input, out int inputInt))
        {
            PrintError("Invalid Input", "Please enter a valid number");
            return "Error: Not a Number";
        }

        if ((!negativeAllowed && inputInt < 0))
        {
           PrintError("Invalid Input", "Please enter a positive number");
            return "Error: Not a Positive Number";
        }

        //Section for 'number being too high' error handling
        {
            if (string.IsNullOrWhiteSpace(maxNumber.ToString()))
                return input;
                        
            if(inputInt > maxNumber)
            {
                PrintError("Invalid Input", $"Please enter a number less than {maxNumber}");
                return "Error: Input Too High";
            }

        }

        return input;
    }

    public static string? GetString(string message, bool nullAllowed)
    {
        Console.WriteLine(message);
        Console.Write("> ");
        string? input = Console.ReadLine().Trim().ToLower();

        if(!nullAllowed)
        {
            if (string.IsNullOrWhiteSpace(input))
            {
                PrintError("Invalid Input", "Please enter something");
                return "Error: Empty Input";
            }
        }

        return input;
    }
}