using System;
using System.Diagnostics;
using System.Collections.Generic;

public class BBSolveData
{
    public List<BBSolveData> BBSolves = new List<BBSolveData>();
    public TimeSpan Time = new TimeSpan();
    public DateTime Date = new DateTime();

    public BBSolveData(TimeSpan time, DateTime date)
    {
        Time = time;
        Date = date;

        BBSolves.Add(this);
    }
}