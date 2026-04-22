using System;
using System.Linq;
using System.Diagnostics;
using System.Collections.Generic;
namespace S_Cube;

public static partial class Solves
{
    static Stopwatch time;
    static string solvesFolder;
    static SolveData? openedSolve;
    public static bool WriteSolve = true;

    public static (string option, Action action)[] options =
    {
        ("Exit", null),
        ("Do Solve", Do)
    };

    public static void Home()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine($"{Saving.AppName} \nSOLVES\n");

            Console.WriteLine($"Average Time: {SolveData.AverageTime.ToString(@"mm\:ss\.fff")}\n");
            for (int i = 0; i < options.Length; i++)
            {
                Console.WriteLine($"{i}. {options[i].option}");
            }

            for (int i = options.Length - 1; i < SolveData.Solves.Count + options.Length - 1; i++)
            {
                Console.WriteLine($"{i + 1}. Solve Number: {SolveData.Solves[i - options.Length + 1].Number}");
            }

            Console.WriteLine($"\n(Enter {options.Length} to view Solve number 1)");
            string inputStr = MainMenu.GetNumber(false, (int)SolveData.Amount + options.Length - 1);


            if (inputStr.StartsWith("Error"))
                continue;

            int input = int.Parse(inputStr);

            if (input == 0)
                return;
                
            if (input < options.Length)
            {
                options[input].action();
                continue;
            }

            openedSolve = SolveData.Solves[input - options.Length];
            Open();
        }
    }

    public static void Do()
    {
        string solvesFolder = "";

        while (true)
        {
            Console.Clear();

            string? scramble = "No Scramble Provided";

            Console.WriteLine($"{Saving.AppName} \nDO SOLVE \n");
            Console.WriteLine("Enter 'S' to generate a random scramble");
            Console.WriteLine("Enter 'Esc' or 'E' or Exit");
            Console.WriteLine("Enter any other key to start");
            Console.WriteLine("After you started, enter space to add a lap");

            FlushInput();
            ConsoleKeyInfo key = Console.ReadKey();

            if (key.Key == ConsoleKey.S)
            {
                scramble = ScrambleGenerator.GenerateScramble();
                Console.WriteLine($"\nScramble: {scramble}");
                Console.WriteLine("(Enter any key to continue)");
                Console.ReadKey();
            }

            else if (key.Key == ConsoleKey.Escape || char.ToUpperInvariant(key.KeyChar) == 'E')
                return;

            Console.Clear();

            if (((BoolPr)Settings.Preferences["Inspection"]).Value == true)
                Inspection();

            DateTime currentDate = DateTime.Now;
            time = Stopwatch.StartNew();

            MainMenu.Stats.TimeSpentSolving.Start();
            MainMenu.Stats.AdvanceSolvesTimer.Start();
            
            while (true)
            {
                Console.Clear();

                if (Console.KeyAvailable)
                    break;
        
                Console.WriteLine("Time: " + time.Elapsed.ToString(@"mm\:ss\.ff"));
                Thread.Sleep(100);
            }

            FlushInput();
            MainMenu.Stats.TimeSpentSolving.Stop();
            MainMenu.Stats.AdvanceSolvesTimer.Stop();
            time.Stop();

            Console.Clear();

            Console.WriteLine($"Total Time: {time.Elapsed.ToString(@"mm\:ss\.fff")}");
            Console.WriteLine("(Enter 'D' to delete this solve)");
            Console.WriteLine("(Enter '2' to mark thsi solve as +2)");
            Console.WriteLine("(Enter 'F' to mark this solve as Did Not Finish)");
            Console.WriteLine("(Enter any key to continue)");

            Thread.Sleep(1500);
            char input = char.ToUpperInvariant(Console.ReadKey(true).KeyChar);

            bool delete = false;
            Penalty penalty = Penalty.None;
            
            if (input == 'D' && ConfirmDeletion())
                delete = true;

            else if(input == '2')
            {
                penalty = Penalty.Plus2;
            }

            else if(input == 'F')
            {
                penalty = Penalty.DNF;
            }

            else if(delete)
            {
                continue;
            }

            SolveData solve = new SolveData(time.Elapsed, scramble, "No Description Provided", currentDate, solvesFolder, penalty);

            if(WriteSolve)
                Saving.WriteSolve(solve);
        }
    }

    public static void Inspection()
    {
        Stopwatch inspection = new Stopwatch();
        inspection.Start();

        while (inspection.Elapsed.Seconds < 15)
        {
            Console.Clear();

            Console.WriteLine("INSPECTION");
            Console.WriteLine("(You can turn this off in the settings)");

            Console.WriteLine(15 - inspection.Elapsed.Seconds);
            Thread.Sleep(1000);
        }

        FlushInput();
        Console.Beep(500, 500);
    }

    public static bool ConfirmDeletion()
    {
        Console.Clear();
        Console.WriteLine("ARE YOU SURE YOU WANT TO DELETE THIS SOLVE. IT CANNOT BE UNDONE.");  
        Console.WriteLine("Enter 'Y' to to confirm deletion");
        Console.WriteLine("Enter anything else to not delete this solve");

        char key = char.ToUpperInvariant(Console.ReadKey(true).KeyChar);

        if(key == 'Y')
            return true;
        return false;
    }

    public static void FlushInput()
    {
        while(Console.KeyAvailable)
            Console.ReadKey(true);
    }
}