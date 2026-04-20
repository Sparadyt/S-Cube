using System;
using System.Diagnostics;
using System.Collections.Generic;
namespace S_Cube;

public class StatsData
{
    public Dictionary<string, Stopwatch> StopwatchesForMods { get; set; } = new Dictionary<string, Stopwatch>();
    public Stopwatch TimeUsed { get; set; } = new Stopwatch();
    public Stopwatch TimeSpentSolving { get; set; } = new Stopwatch();
    public Stopwatch BBSolvesTimer { get; set; } = new Stopwatch();
    public Stopwatch AdvanceSolvesTimer { get; set; } = new Stopwatch();

    public Dictionary<string, int> IntsForMods { get; set; } = new Dictionary<string, int>();
    public int SolvesUnder30Second { get; set; } = 0;
    public int SolvesUnder10Second { get; set; } = 0;

    public Dictionary<string, double> DoublesForMods { get; set; } = new Dictionary<string, double>();


    public Dictionary<string, bool> BoolsForMods{ get; set; }  = new Dictionary<string, bool>();
    public bool NewUser { get; set; } = false;
}