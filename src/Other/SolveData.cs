using System;
using System.Collections.Generic;
namespace S_Cube;

public class SolveData
{
    public static List<SolveData> Solves = new List<SolveData>();
    public int Number { get; private set; }
    public static int Amount { get; private set; }
    public TimeSpan Time { get; set; }
    public static TimeSpan AverageTime { get; private set; }
    public string? Description { get; set; }
    public DateTime? Date {get; set;}
    public string? Scramble {get; set;}
    public string? SolvesFolder {get; set;}
    public Penalty? Penalty {get; set;}
    public string? UsedAlgorithm  {get; set;} = ((StringPr)Settings.Preferences["Default Algorithm"]).Value;

    public SolveData(TimeSpan time, string? scramble, string? description, DateTime date, string solvesFolder, Penalty penalty)
    {
        Amount++;
        this.Number = Amount;
        Time = time;
        Scramble = scramble;
        Description = description;
        Date = date;
        SolvesFolder = solvesFolder;
        Penalty = penalty;

        Solves.Add(this);
        AverageTime =
            TimeSpan.FromMilliseconds(Solves.Average(s => s.Time.TotalMilliseconds));
    }

    public bool IsUnderChosenSeconds(int time)
    {
        if(this.Time.Seconds < time)
            return true;
        return false;
    }
}

public enum Penalty
{
    None,
    Plus2,
    DNF
}