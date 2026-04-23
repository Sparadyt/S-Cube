using System;
using System.Diagnostics;
using System.Collections.Generic;
namespace S_Cube;

//BB stands for 'Bare Bones'
public class BBSolveData
{
    public static List<BBSolveData> Solves = new List<BBSolveData>();
    public static long Amount { get; private set; } = 0;
    public long Number { get; private set; }
    public TimeSpan Time { get; set; } = new TimeSpan();
    public static TimeSpan Mean { get; set; } = new TimeSpan();
    public DateTime Date { get; set; } = new DateTime();

    public BBSolveData()
    {
        Amount++;
        Number = Amount;

        Solves.Add(this);

        Mean =
            TimeSpan.FromMilliseconds(Solves.Average(s => s.Time.TotalMilliseconds));
    }
    public BBSolveData(TimeSpan time, DateTime date)
    {
        Time = time;
        Date = date;
        Amount++;
        Number = Amount;

        Solves.Add(this);

        Mean =
            TimeSpan.FromMilliseconds(Solves.Average(s => s.Time.TotalMilliseconds));
    }

    public bool IsUnderChosenSeconds(int time)
    {
        if(this.Time.Seconds < time)
            return true;
        return false;
    }
}