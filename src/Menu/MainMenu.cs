using System;
using System.Linq;
using System.Threading;
using System.Diagnostics;
namespace S_Cube;

public static class MainMenu
{
    public static StatsData Stats = new StatsData();

    static void Main()
    {
        (string option, Action action)[] options =
        {
            ("Exit", () => Environment.Exit(0)),
            ("Solves", Solves.Home),
            ("Do Solve", Solves.Do),
            ("Bare-Bones Solves", BBSolves.Home),
            ("Do Bare-Bones Solve", BBSolves.Do),
            ("Stats", SeeStats.Home),
            ("Settings", Settings.Home),
            ("Info", Info.Home)
        };

        Saving.CreateFiles();
        Saving.UpdateStats();
        Saving.UpdateValues();
        CheckForErrors();

        Thread bgThread = new Thread(BackgroundWork);
        bgThread.Start();

        Stats.TimeUsed.Start();
        while (true)
        {
            Console.Clear();
            Console.WriteLine("S-Cube \nHOME\n");

            for (int i = 0; i < options.Length; i++)
            {
                Console.WriteLine($"{i}. {options[i].option}");
            }

            string inputStr = GetNumber(false, options.Length - 1, true);

            //Handling Input
            if (inputStr.StartsWith("Error"))
                continue;

            options[int.Parse(inputStr)].action();
        }
    }

    public static void PrintError(string title, string message, bool devError = false)
    {
        Console.ForegroundColor = ConsoleColor.Red;

        if (!devError)
            Console.WriteLine($"Error: {title}");
        else
            Console.WriteLine($"Dev Error: {title}");

        Console.WriteLine(message);

        if(devError)
        {
            Console.WriteLine("(Dex Error is the fault of the developer. If you want to, you can help the developer of making a issue in the Github page)");
        }
        Console.WriteLine("(Enter any key to continue)");
        Console.ReadKey();
        Console.ResetColor();
        Console.Clear();
    }

    public static string GetNumber(bool negativeAllowed, long? maxNumber,  bool getKey = false)
    {
        string? input = "";

        Console.WriteLine("Enter a number");
        Console.Write("> ");

        if (!getKey)
        {
            input = Console.ReadLine();
        }

        else
        {
            input = Console.ReadKey().KeyChar.ToString();
        }

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

        if (!nullAllowed)
        {
            if (string.IsNullOrWhiteSpace(input))
            {
                PrintError("Invalid Input", "Please enter something");
                return "Error: Empty Input";
            }
        }

        return input;
    }

    public static void CheckForErrors()
    {
        //
    }

    public static void BackgroundWork()
    {
        IntPr intPreference = (IntPr)Settings.Preferences["Ms Interval"];
        SaveStats(intPreference.Value);
    }

    public static async Task SaveStats(int msInterval)
    {
        while (true)
        {
            Saving.SaveStats(Stats);
            Thread.Sleep(msInterval);
        }
    }
}