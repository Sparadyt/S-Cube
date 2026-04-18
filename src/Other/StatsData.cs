using System;
using System.Diagnostics;
using System.Collections.Generic;
namespace S_Cube;

public class StatsData
{
    public Dictionary<string, Stopwatch> StopwatchesForMods = new Dictionary<string, Stopwatch>();
    public Stopwatch TimeUsed = new Stopwatch();
    public Stopwatch TimeSpentSolving = new Stopwatch();
    public Stopwatch BBSolvesTimer = new Stopwatch();
    public Stopwatch AdvanceSolvesTimer = new Stopwatch();

    public Dictionary<string, int> IntsForMods = new Dictionary<string, int>();
    public int SolvesUnder30Second = 0;
    public int SolvesUnder10Second = 0;

    public Dictionary<string, double> DoublesForMods = new Dictionary<string, double>();


    public Dictionary<string, bool> BoolsForMods = new Dictionary<string, bool>();
    public bool NewUser = false;
}