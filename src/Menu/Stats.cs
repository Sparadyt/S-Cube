using System;
using System.Collections.Generic;
namespace S_Cube;

public static class SeeStats
{
    public static void Home()
    {
        Console.Clear();
        Console.WriteLine("S-Cube \nSTATS\n");

        ShowTotalTimeUsed();
        ShowTotalSolves();
        ShowAverageTime();
        ShowBestTime();
        ShowWorstTime();

        Console.WriteLine("\n(Enter any key to Exit)");
        Console.ReadKey();
    }

    static void ShowTotalTimeUsed()
    {
        Console.WriteLine($"Time Used: {MainMenu.Stats.TimeUsed.Elapsed.ToString(@"d\:hh\:mm\:ss")}");
        Console.WriteLine($"Time Spent Solving: {MainMenu.Stats.TimeSpentSolving.Elapsed.ToString(@"d\:hh\:mm\:ss")}");
        Console.WriteLine($"Time Spent Doing Advance Solves: {MainMenu.Stats.AdvanceSolvesTimer.Elapsed.ToString(@"d\:hh\:mm\:ss")}");
        Console.WriteLine($"Time Spent Doing Bare-Bones Solves: {MainMenu.Stats.BBSolvesTimer.Elapsed.ToString(@"d\:hh\:mm\:ss")}");
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