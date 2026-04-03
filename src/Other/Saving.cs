using System;
using System.IO;
using System.Text.Json;
using System.Collections.Generic;

public static class Saving
{
    static string? localProjectPath = "";
    static string? roamingProjectPath = "";
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
            localProjectPath = Path.Combine(localPath, appName);
        }

        string allSolvesPath = Path.Combine(localProjectPath, "AllSolves");
        CreateFolder(allSolvesPath);

        bareBoneSolvesPath = Path.Combine(allSolvesPath, "BareBoneSolves");
        CreateFolder(bareBoneSolvesPath);

        advanceSolvesPath = Path.Combine(allSolvesPath, "AdvanceSolves");
        CreateFolder(advanceSolvesPath);

        if (!Path.Exists(Path.Combine(roamingPath, appName)))
        {
            Directory.CreateDirectory(Path.Combine(roamingPath, appName));
            roamingProjectPath = Path.Combine(localPath, appName);
        }

        statsPath = Path.Combine(roamingProjectPath, "Stats.json");
        CreateFolder(statsPath);
    }

    static void CreateFolder(string path)
    {
        if (!Path.Exists(path))
        {
            Directory.CreateDirectory(path);
        }
    }
}