using System;
using System.Collections.Generic;

public static class Stats
{
    public static void Home()
    {
        Console.Clear();
        Console.WriteLine("S-Cube \nSTATS\n");

        ShowTotalSolves();
        ShowAverageTime();
        ShowBestTime();
        ShowWorstTime();

        Console.WriteLine("\n(Enter any key to Exit)");
        Console.ReadKey();
    }

    static void ShowTotalSolves()
    {
        Console.WriteLine($"Total Solves: {SolveData.Amount}");
    }

    static void ShowAverageTime()
    {
        Console.Write("Average Time: ");
        if (SolveData.Amount == 0)
        {
            Console.WriteLine("N/A");
            return;
        }

        Console.WriteLine(SolveData.AverageTime.ToString(@"mm\:ss\.fff"));
    }

    //To Do: Add AO5 and AO12

    static void ShowBestTime()
    {
        Console.Write("Best Time: ");
        if (SolveData.Solves.Count == 0)
        {
            Console.WriteLine("N/A");
            return;
        }

        TimeSpan bestTime = TimeSpan.MaxValue;
        foreach (var solve in SolveData.Solves)
        {
            if (solve.Time < bestTime)
            {
                bestTime = solve.Time;
            }
        }

        Console.WriteLine(bestTime.ToString(@"mm\:ss\.fff"));
    }

    static void ShowWorstTime()
    {
        Console.Write("Worst Time: ");
        if (SolveData.Solves.Count == 0)
        {
            Console.WriteLine("N/A");
            return;
        }

        TimeSpan worstTime = TimeSpan.MinValue;
        foreach (var solve in SolveData.Solves)
        {
            if (solve.Time > worstTime)
            {
                worstTime = solve.Time;
            }
        }
        
        Console.WriteLine(worstTime.ToString(@"mm\:ss\.fff"));
    }
}