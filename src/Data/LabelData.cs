using System;
using System.Collections.Generic;
namespace S_Cube;

public class LabelData
{
    public string Cube;
    public bool? Practice;
    public string? Event;
    public List<string> OtherLabels = new List<string>();

    public LabelData()
    {
        Cube = ((StringPr)Settings.Preferences["Default Cube"]).Value;
        Practice = ((BoolPr)Settings.Preferences["Practice Mode"]).Value;
        Event = ((StringPr)Settings.Preferences["Default Event"]).Value;
    }

    public LabelData(string cube, bool practice, string cubingEvent, List<string> otherLabels)
    {
        Cube = cube;
        Practice = practice;
        Event = cubingEvent;
        OtherLabels = otherLabels;
    }
}