using System;
 using System.Diagnostics;
 using System.Collections.Generic;
 namespace S_Cube;

public class StatsData
{
    public Stopwatch TimeUsed = new Stopwatch();
    public Stopwatch TimeSpentSolving = new Stopwatch();
    public Stopwatch BBSolvesTimer = new Stopwatch();
    public Stopwatch AdvanceSolvesTimer = new Stopwatch();

    public int SolvesUnder30Second = 0;
    public int SolvesUnder10Second = 0;

    public bool NewUser = false;
 }