using System;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;

public static partial class Solves
{
    private static Stopwatch time;
    private static List<List<SolveData.LapData>> laps = new List<List<SolveData.LapData>>();
    static int lapNum = 0;
    private static string solvesFolder;
    private static bool isRunning;
    private static SolveData openedSolve;

    private static (string option, Action action)[] options =
    {
        ("Exit", () => Environment.Exit(0)),
        ("Do Solve", Do)
    };

    public static void Home()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("S-Cube \nSOLVES\n");

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

            int input = Convert.ToInt32(inputStr);

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
        string solvesFolder = @"S-Cube/All Solves/Default";

        while (true)
        {
            Console.Clear();
            laps.Add(new List<SolveData.LapData>());

            string? scramble = "No Scramble Provided";

            Console.WriteLine("S-Cube \nDO SOLVE\n");
            Console.WriteLine("Enter 'S' to generate a random scramble");
            Console.WriteLine("Enter 'Esc' or 'E' or Exit");
            Console.WriteLine("Enter any other key to start");
            Console.WriteLine("After you started, enter space to add a lap");
            ConsoleKeyInfo key = Console.ReadKey();

            if (key.Key == ConsoleKey.S)
            {
                scramble = GenerateScramble();
                Console.WriteLine($"\nScramble: {scramble}");
                Console.WriteLine("(Enter any key to continue)");
                Console.ReadKey();
            }

            else if (key.Key == ConsoleKey.Escape || (key.KeyChar.ToString().ToLower()) == "e")
            {
                return;
            }

            Console.Clear();
            isRunning = true;
            DateTime currentDate = DateTime.Now;

            time = Stopwatch.StartNew();
            TimeSpan previousTime = new TimeSpan();
            int millisecondToWait = 500;

            Thread lapThread = new Thread(Laps);
            lapThread.Start();

            while (isRunning)
            {
                Console.Clear();

                Console.WriteLine("Enter 'Space' to make a new lap");
                for (int i = 0; i < laps[lapNum].Count; i++)
                {
                    Console.WriteLine($"{laps[lapNum][i].Name}: {laps[lapNum][i].Time?.ToString(@"mm\:ss\:ff")}");
                }
                Console.WriteLine();

                if (time.Elapsed.Milliseconds >= millisecondToWait)
                {
                    Console.WriteLine("Time: " + time.Elapsed.ToString(@"mm\:ss\.ff"));
                    previousTime = time.Elapsed;
                }

                else
                {
                    Console.WriteLine("Time: " + previousTime.ToString(@"mm\:ss\.ff"));
                }

                Thread.Sleep(50);
            }

            time.Stop();

            Console.Clear();

            for (int i = 0; i < laps[lapNum].Count; i++)
            {
                Console.WriteLine($"{laps[lapNum][i].Name}: {laps[lapNum][i].Time?.ToString(@"mm\:ss\.fff")}");
            }

            Console.WriteLine($"Total Time: {time.Elapsed.ToString(@"mm\:ss\.fff")}");
            Console.WriteLine("(Enter any key to continue)");

            SolveData solve = new SolveData(time.Elapsed, scramble, "No Description Provided", currentDate, solvesFolder, laps[lapNum]);
            
            Console.ReadKey();
            Console.Clear();

            lapNum++;
        }
    }

    static string GenerateScramble()
    {
        Console.Clear();
        char previousMove = '0';
        char move = 'A';
        char[] moves = { 'R', 'L', 'U', 'D', 'F', 'B' };
        string[] modifiers = { "", "'", "2", "w" };

        string scramble = "";

        for (int i = 0; i < MainMenu.rand.Next(20, 26); i++)
        {
            while (true)
            {
                move = moves[MainMenu.rand.Next(moves.Length)];

                if (move == previousMove)
                    continue;

                else
                    break;
            }

            previousMove = move;
            string modifier = modifiers[MainMenu.rand.Next(modifiers.Length)];
            scramble += move + modifier + " ";
        }

        return scramble.Trim();
    }

    static void Laps()
    {
        while (true)
        {
            ConsoleKeyInfo key = Console.ReadKey();

            if (key.Key == ConsoleKey.Spacebar)
            {
                laps[lapNum].Add(new SolveData.LapData(time.Elapsed, $"Lap {laps[lapNum].Count + 1}"));
            }

            else
            {
                isRunning = false;
                return;
            }
        }
    }
}