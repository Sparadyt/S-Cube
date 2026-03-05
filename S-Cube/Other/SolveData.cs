using System;
using System.Collections.Generic;

class SolveData
{
    public static List<SolveData> Solves = new List<SolveData>();
    public int Number { get; private set; }
    public static int Amount { get; private set; }
    readonly TimeSpan time;
    public static TimeSpan AverageTime { get; private set; }
    readonly DateTime? date;
    public string? Description;
    readonly string? scramble;
    struct LapData
    {
        public readonly TimeSpan? Time;
        public string? Name;

        public LapData(TimeSpan? Time, string? Name)
        {
            this.Time = Time;
            this.Name = Name;
        }
    }

    readonly List<LapData> laps = new List<LapData>();
    public string solvesFolder;

    public SolveData(TimeSpan time, string? scramble, string? description, DateTime date)
    {
        Amount++;
        this.Number = Amount;
        this.time = time;
        this.scramble = scramble;
        this.Description = description;
        this.date = date;

        Solves.Add(this);
        AverageTime =
            TimeSpan.FromMilliseconds(Solves.Average(s => s.time.TotalMilliseconds));
    }
}