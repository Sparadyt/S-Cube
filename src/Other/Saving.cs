using System;
using System.IO;
using System.Text.Json;
using System.Collections.Generic;

public static class Saving
{
    public static string? LocalProjectPath { get; private set; } = "";
    public static string? RoamingProjectPath { get; private set; } = "";
    static string appName = "S-Cube";
    static string? bareBoneSolvesPath = "";
    static string? advanceSolvesPath = "";
    static string? statsPath = "";

    public static void CreateFiles()
    {
        string localPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
        string roamingPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);

        if (!Path.Exists(Path.Combine(localPath, appName)))
        {
            Directory.CreateDirectory(Path.Combine(localPath, appName));
            LocalProjectPath = Path.Combine(localPath, appName);
        }

        string allSolvesPath = Path.Combine(LocalProjectPath, "AllSolves");
        CreateFolder(allSolvesPath);

        bareBoneSolvesPath = Path.Combine(allSolvesPath, "BareBoneSolves");
        CreateFolder(bareBoneSolvesPath);

        advanceSolvesPath = Path.Combine(allSolvesPath, "AdvanceSolves");
        CreateFolder(advanceSolvesPath);

        if (!Path.Exists(Path.Combine(roamingPath, appName)))
        {
            Directory.CreateDirectory(Path.Combine(roamingPath, appName));
            RoamingProjectPath = Path.Combine(localPath, appName);
        }

        statsPath = Path.Combine(RoamingProjectPath, "Stats.json");
        CreateFolder(statsPath);
    }

    static void CreateFolder(string path)
    {
        if (!Path.Exists(path))
        {
            Directory.CreateDirectory(path);
        }
    }

    static void SaveBBSolve(BBSolveData solve)
    {
        var options = new JsonSerializerOptions
        {
            WriteIndented = true
        };
    }
}