using System;
using System.Linq;
using System.Diagnostics;
using System.Collections.Generic;

public static class BBSolves
{
    static BBSolveData openedSolve;
    static bool isRunning = true;

    public static void Home()
    {
        (string option, Action action)[] options =
        {
            ("Exit", null),
            ("Do Solve", Do)
        };

        while (true)
        {
            Console.Clear();
            Console.WriteLine("S-Cube \nSOLVES\n");

            Console.WriteLine($"Average Time: {SolveData.AverageTime.ToString(@"mm\:ss\.fff")}\n");
            for (int i = 0; i < options.Length; i++)
            {
                Console.WriteLine($"{i}. {options[i].option}");
            }

            for (int i = options.Length - 1; i < BBSolveData.Solves.Count + options.Length - 1; i++)
            {
                Console.WriteLine($"{i + 1}. Solve Number: {BBSolveData.Solves[i - options.Length + 1].Number}");
            }

            Console.WriteLine($"\n(Enter {options.Length} to view Solve number 1)");
            string inputStr = MainMenu.GetNumber(false, BBSolveData.Amount + options.Length - 1);


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

            openedSolve = BBSolveData.Solves[input - options.Length];
            Open();
        }
    }

    public static void Do()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("S-Cube \nDO BARE-BONES SOLVE \n");
            Console.WriteLine($"Scramble: {ScrambleGenerator.GenerateScramble()}");
            Console.WriteLine("Enter 'Esc' or 'E' to Exit");
            Console.WriteLine("Enter any other key to start");

            ConsoleKeyInfo key = Console.ReadKey();

            if (key.Key == ConsoleKey.Escape || (key.KeyChar.ToString().ToLower()) == "e")
                return;

            Console.Clear();

            DateTime currentDate = DateTime.Now;
            Stopwatch time = new Stopwatch();
            time.Start();

            Thread lapThread = new Thread(Stop);
            lapThread.Start();

            isRunning = true;
            while (isRunning)
            {
                Console.Clear();

                Console.WriteLine("Time: " + time.Elapsed.ToString(@"mm\:ss\.ff"));
                Thread.Sleep(100);
            }

             Console.WriteLine($"Total Time: {time.Elapsed.ToString(@"mm\:ss\.fff")}");
            Console.WriteLine("(Enter any key to continue)");

            BBSolveData solve = new BBSolveData(time.Elapsed, currentDate);
            Saving.SaveBBSolve(solve);
            Console.ReadKey();
        }
    }

    static void Stop()
    {
        Console.ReadKey(true);
        isRunning = false;
    }

    static void Open()
    {
        Console.Clear();
        Console.WriteLine($"BB Solve Number: {openedSolve.Number}");
        Console.WriteLine($"Time: {openedSolve.Time.ToString(@"mm\:ss\.fff")}");
        Console.WriteLine($"Date: {openedSolve.Date}");
        Console.WriteLine("(Enter any key to continue)");
        Console.ReadKey();
    }
}