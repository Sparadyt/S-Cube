using System;
using System.Linq;
using NetCoreAudio;
using System.Diagnostics;
using System.Collections.Generic;
namespace S_Cube;

public static partial class Solves
{
    static Stopwatch time;
    static string solvesFolder;
    static SolveData? openedSolve;

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

            Console.WriteLine($"Average Time: {SolveData.Mean.ToString(@"mm\:ss\.fff")}\n");
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
        while (true)
        {
            Console.Clear();

            string? scramble = "No Scramble Provided";

            Console.WriteLine($"{Saving.AppName} \nDO SOLVE \n");
            Console.WriteLine("Enter 'S' to generate a random scramble");
            Console.WriteLine("Enter 'Esc' or 'E' or Exit");
            Console.WriteLine("Enter any other key to start");
            Console.WriteLine("After you started, enter space to add a lap");

            CrossSolves.Wait(1000);
            ConsoleKeyInfo key = Console.ReadKey();

            if (key.Key == ConsoleKey.S)
            {
                scramble = ScrambleGenerator.GenerateScramble();
                ScrambleGenerator.ShowScramble(scramble);
            }

            else if (key.Key == ConsoleKey.Escape || char.ToUpperInvariant(key.KeyChar) == 'E')
                return;

            Console.Clear();

            if (((BoolPr)Settings.Preferences["CrossSolvess.Inspection"]).Value)
                CrossSolves.Inspection();

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

            CrossSolves.FlushInput();
            MainMenu.Stats.TimeSpentSolving.Stop();
            MainMenu.Stats.AdvanceSolvesTimer.Stop();
            time.Stop();

            Console.Clear();

            Console.WriteLine($"Total Time: {time.Elapsed.ToString(@"mm\:ss\.fff")}");
            Console.WriteLine("(Enter 'D' to delete this solve)");
            Console.WriteLine("(Enter '2' to mark thsi solve as +2)");
            Console.WriteLine("(Enter 'F' to mark this solve as Did Not Finish)");
            Console.WriteLine("(Enter any key to continue)");

            CrossSolves.Wait(1000);
            char input = char.ToUpperInvariant(Console.ReadKey(true).KeyChar);

            Penalty penalty = Penalty.None;
            
            if(input == '2')
            {
                penalty = Penalty.Plus2;
            }

            else if(input == 'F')
            {
                penalty = Penalty.DNF;
            }

            else if(input == 'D' && CrossSolves.ConfirmDeletion())
            {
                continue;
            }

            SolveData solve = new SolveData(time.Elapsed, scramble, "No Description Provided", currentDate, null, penalty);

            if(CrossSolves.WriteSolve)
                Saving.WriteSolve(solve);
        }
    }

    public static TimeSpan CalculateMean(List<SolveData> solves, List<string> allowedLabels, List<string> requiredLabels, List<string> excludesLabels)
    {
        //List<SolveData> filteredSolves = solves.Where(solve=> ).ToList();
        return new TimeSpan();
    }
}