using System;
using System.Collections.Generic;

class SolveData
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
    public string? UsedAlgorithm;
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

    public SolveData(TimeSpan time, string? scramble, string? description, DateTime date, string solvesFolder, List<LapData>? laps)
    {
        Amount++;
        this.Number = Amount;
        this.Time = time;
        this.Scramble = scramble;
        this.Description = description;
        this.Date = date;
        this.SolvesFolder = solvesFolder;
        this.Laps = laps;

        Solves.Add(this);
        AverageTime =
            TimeSpan.FromMilliseconds(Solves.Average(s => s.Time.TotalMilliseconds));
    }
}