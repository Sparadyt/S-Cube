using System;
using System.Linq;
using System.Diagnostics;
using System.Collections.Generic;
namespace S_Cube;

//BB stands for 'Bare Bones'
public class BBSolveData
{
    public static List<BBSolveData> Solves = new List<BBSolveData>();
    public static int Amount { get; private set; } = 0;
    public int Number { get; private set; }
    public TimeSpan Time { get; set; } = new TimeSpan();
    public static TimeSpan Mean { get; set; } = new TimeSpan();
    public DateTime Date { get; set; } = new DateTime();
    public LabelData? Labels;
    public string Path { get; set; }

    public BBSolveData()
    {
        Number = Solves.Count - 1;
        Solves.Add(this);

        Mean = CalculateMean(Solves);
    }
    
    public BBSolveData(TimeSpan time, DateTime date, LabelData? labels, Penalty penalty)
    {
        Number = Solves.Count;

        Time = time;
        Date = date;
        Labels = labels;

        Solves.Add(this);
        Mean = CalculateMean(Solves);
    }

    public bool IsUnderChosenSeconds(int time)
    {
        if(this.Time.Seconds < time)
            return true;
        return false;
    }

    public static TimeSpan CalculateMean(List<BBSolveData> solves)
    {
        List<double> msTimes = new List<double>();

        foreach (BBSolveData solve in solves)
        {
            msTimes.Add(solve.Time.TotalMilliseconds);
        }

        double msAverage = msTimes.Average();
        return TimeSpan.FromMilliseconds(msAverage);
    }
}