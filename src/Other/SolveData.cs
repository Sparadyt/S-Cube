using System;
using System.Collections.Generic;
namespace S_Cube;

public class SolveData
{
    public static List<SolveData> Solves = new List<SolveData>();
    public int Number { get; private set; }
    public static int Amount { get; private set; }
    public TimeSpan Time;
    public static TimeSpan AverageTime { get; private set; }
    public string? Description;
    public DateTime? Date;
    public string? Scramble;
    public string? SolvesFolder;
    public Penalty? Penalty;
    public string? UsedAlgorithm = ((StringPr)Settings.Preferences["Default Algorithm"]).Value;
    public class LapData
    {
        public readonly TimeSpan? Time;
        public string? Name;

        public LapData(TimeSpan? Time, string? Name)
        {
            this.Time = Time;
            this.Name = Name;
        }
    }

    public List<LapData> Laps = new List<LapData>();

    public SolveData(TimeSpan time, string? scramble, string? description, DateTime date, string solvesFolder, Penalty penalty, List<LapData>? laps)
    {
        Amount++;
        this.Number = Amount;
        Time = time;
        Scramble = scramble;
        Description = description;
        Date = date;
        SolvesFolder = solvesFolder;
        Penalty = penalty;
        Laps = laps;

        Solves.Add(this);
        AverageTime =
            TimeSpan.FromMilliseconds(Solves.Average(s => s.Time.TotalMilliseconds));
    }
}

public enum Penalty
{
    None,
    Plus2,
    DNF
}