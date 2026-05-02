using System;
using System.IO;
using System.Linq;
using NetCoreAudio;
using System.Threading;
using System.Diagnostics;
namespace S_Cube;

public static class MainMenu
{
    public static StatsData Stats = new StatsData();

    public static List<(string name, Action action)> Options = new List<(string option, Action action)>
    {
        ("Exit", () => Environment.Exit(0)),
        ("Solves", Solves.Home),
        ("Do Solve", Solves.Do),
        ("Bare-Bones Solves", BBSolves.Home),
        ("Do Bare-Bones Solve", BBSolves.Do),
        ("Learn Cubing", OpenLearnCubing),
        ("Stats", SeeStats.Home),
        ("Settings", Settings.Home),
        ("Info", Info.Home)
    };

    static void Main()
    {
        Console.Clear();

        Saving.CreateFiles();
        Saving.UpdateStats();
        Saving.SaveStats(Stats);
        Saving.UpdateValues();
        Saving.SavePreferences(Settings.Preferences);

        Thread bgThread = new Thread(BackgroundWork);
        bgThread.Start();

        Stats.TimeUsed.Start();

        bool inputIsKey = true;
        if(Options.Count > 10)
            inputIsKey = false;

        CrossSolves.Algorithms.Add("No Algorithm");

        while (true)
        {
            Console.Clear();
            Console.WriteLine($"{Saving.AppName} \nHOME\n");

            for (int i = 0; i < Options.Count; i++)
            {
                Console.WriteLine($"{i}. {Options[i].name}");
            }

            string inputStr = GetNumber(false, Options.Count - 1, inputIsKey);

            //Handling Input
            if (inputStr.StartsWith("Error"))
                continue;

            Options[int.Parse(inputStr)].action();
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

        if (devError)
        {
            Console.WriteLine("(Dev Error might be the fault of the developer. If you want to, you can help the developer of making a issue in the Github page)");
        }

        Console.WriteLine("(Enter any key to continue)");
        Console.ResetColor();
        Console.ReadKey();
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

    public static void BackgroundWork()
    {
        SaveStats();
    }

    public static async Task SaveStats()
    {
        while (true)
        {
            Saving.SaveStats(Stats);
            await Task.Delay(((IntPr)Settings.Preferences["Ms Interval"]).Value);
        }
    }

    public static async Task PlaySFX(string fileName)
    {
        var player = new Player();
        await player.SetVolume(Byte.Parse(((IntPr)Settings.Preferences["SFX Volume"]).Value.ToString()));
        await player.Play(Path.Combine(Saving.AudioPath, fileName));
    }

    public static void OpenLearnCubing()
    {
        //string filePath = Path.Combine("Docs", "Learn Cubing", "Beginner", "Start.html");
        string filePath = Path.Combine(Saving.ProjectDir, "Docs", "Learn Cubing", "Beginner", "Start.html");

        if (!File.Exists(filePath))
        {
            PrintError("File Not Found", "The file for Learn Cubing could not be found. Please make sure the file exists and try again.", true);
            return;
        }

        Process.Start(new ProcessStartInfo
        {
            FileName = filePath,
            UseShellExecute = true
        });

        Console.Clear();
        string dots = ".";

        while(true)
        {
            Console.Clear();
            if (Console.KeyAvailable)
            {
                Console.ReadKey(true);
                break;
            }

            Console.WriteLine("Opening" + dots);
            Console.WriteLine("(Enter any key oncce it's open)");

            switch (dots)
            {
                case ".":
                    dots = "..";
                    break;
                case "..":
                    dots = "...";
                    break;
                case "...":
                    dots = ".";
                    break;
            }
            
            Thread.Sleep(500);
        }
    }
}