using System;
 using System.Diagnostics;
 using System.Collections.Generic;

 public class StatsData
{
    public Stopwatch TimeUsed = new Stopwatch();
    public Stopwatch TimeSpentSolving = new Stopwatch();
    public Stopwatch BBSolvesTimer = new Stopwatch();
    public Stopwatch AdvanceSolvesTimer = new Stopwatch();

    public bool NewUser = false;
 }