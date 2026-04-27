using System;
using System.Collections.Generic;

public class LabelData
{
    public string Cube;
    public bool? Practice;
    public List<string> OtherLabels = new List<string>();

    public LabelData(string cube, bool practice, List<string> otherLabels)
    {
        Cube = cube;
        Practice = practice;
        OtherLabels = otherLabels;
    }
}