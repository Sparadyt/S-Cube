using System;
using System.Collections.Generic;
namespace S_Cube;

public record HotkeyData
{
    public static List<HotkeyData> Hotkeys = new List<HotkeyData>();
    public Dictionary<string, List<ConsoleKeyInfo>> Combination = new Dictionary<string, List<ConsoleKeyInfo>>();
    public Action Method;

    public HotkeyData(Dictionary<string, List<ConsoleKeyInfo>> combination, Action method)
    {
        Combination = combination;
        Method = method;
    }
}

public static class Hotkey
{
    public static async Task CheckForHotkeys()
    {
        while(true)
        {
            //
        }
    }
}