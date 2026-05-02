using System;
using System.IO;
using System.Linq;
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
            string inputStr = MainMenu.GetNumber(false, SolveData.Solves.Count + options.Length - 1);

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

            CrossSolves.DoSolve(true, scramble);

            
        }
    }

    public static TimeSpan CalculateMean(List<SolveData> solves, List<string> allowedLabels, List<string> requiredLabels, List<string> excludesLabels)
    {
        //List<SolveData> filteredSolves = solves.Where(solve=> ).ToList();
        return new TimeSpan();
    }

    public static void DeleteSolve(SolveData solve)
    {
        SolveData.Solves.Clear();

        if (Path.Exists(solve.Path))
            File.Delete(solve.Path);

        Saving.UpdateValues();
    }
}