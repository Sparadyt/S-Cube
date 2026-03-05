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
        if (SolveData.Amount == 0)
        {
            Console.WriteLine("Average Time: N/A");
            return;
        }
        
        Console.WriteLine($"Average Time: {SolveData.AverageTime.ToString(@"mm\:ss\.fff")}");
    }

    static void ShowBestTime()
    {
        if (SolveData.Solves.Count == 0)
        {
            Console.WriteLine("Best Time: N/A");
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

        Console.WriteLine($"Best Time: {bestTime.ToString(@"mm\:ss\.fff")}");
    }

    static void ShowWorstTime()
    {
        if (SolveData.Solves.Count == 0)
        {
            Console.WriteLine("Worst Time: N/A");
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
        
        Console.WriteLine($"Best Time: {worstTime.ToString(@"mm\:ss\.fff")}");
    }
}