using System;
using System.Collections.Generic;

class SolveData
{
    public static List<SolveData> Solves = new List<SolveData>();
    public int Number { get; private set; }
    public static int Amount { get; private set; }
    public TimeSpan Time { get; private set; }
    public static TimeSpan AverageTime { get; private set; }
    public string? Description;
    readonly DateTime? date;
    readonly string? scramble;
    public string? SolvesFolder;
    readonly string? usedAlgorithm;
    public struct LapData
    {
        public readonly TimeSpan? Time;
        public string? Name;

        public LapData(TimeSpan? Time, string? Name)
        {
            this.Time = Time;
            this.Name = Name;
        }
    }

    public List<LapData> Laps {get; private set;} = new List<LapData>();
    public string solvesFolder;

    public SolveData(TimeSpan time, string? scramble, string? description, DateTime date)
    {
        Amount++;
        this.Number = Amount;
        this.Time = time;
        this.scramble = scramble;
        this.Description = description;
        this.date = date;
        this.usedAlgorithm = null;
        this.solvesFolder = @"S-Cube/All Solves/Default";

        Solves.Add(this);
        AverageTime =
            TimeSpan.FromMilliseconds(Solves.Average(s => s.Time.TotalMilliseconds));
    }
}