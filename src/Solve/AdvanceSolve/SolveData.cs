using System;
using System.Collections.Generic;
namespace S_Cube;

public class SolveData
{
    public static List<SolveData> Solves = new List<SolveData>();
    public int Number { get; set; }
    public TimeSpan Time { get; set; }
    public static TimeSpan Mean { get; private set; }
    public string? UsedAlgorithm { get; set; } = ((StringPr)Settings.Preferences["Default Algorithm"]).Value;
    public string? Description { get; set; }
    public DateTime? Date {get; set;}
    public string? Scramble {get; set;}
    public LabelData? Labels { get; set; }
    public Penalty? Penalty { get; set; }
    public string Path { get; set; }

    public SolveData()
    {
        Number = Solves.Count;

        Solves.Add(this);
        Mean =
            TimeSpan.FromMilliseconds(Solves.Average(s => s.Time.TotalMilliseconds));
    }
    public SolveData(TimeSpan time, string? scramble, string? description, DateTime date, LabelData labels, Penalty penalty)
    {
        Number = Solves.Count;

        Time = time;
        Scramble = scramble;
        Description = description;
        Date = date;
        Labels = labels;
        Penalty = penalty;

        Solves.Add(this);
    }

    public bool IsUnderChosenSeconds(int time)
    {
        if(Time.Seconds < time)
            return true;
        return false;
    }
}