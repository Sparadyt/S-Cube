using System;
using System.IO;
using System.Text.Json;
using System.Diagnostics;
using System.Collections.Generic;

public static class Saving
{
    static List<string> guids = new List<string>();
    public static string? LocalProjectPath { get; private set; } = "";
    public static string? RoamingProjectPath { get; private set; } = "";
    static string appName = "S-Cube";
    static string? BBSolvesPath = "";
    static string? advanceSolvesPath = "";
    static string? settingsPath = "";

    public static void CreateFiles()
    {
        string localPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
        string roamingPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);

        CreateFolder(Path.Combine(localPath, appName));
        LocalProjectPath = Path.Combine(localPath, appName);

        string allSolvesPath = Path.Combine(LocalProjectPath, "AllSolves");
        CreateFolder(allSolvesPath);

        BBSolvesPath = Path.Combine(allSolvesPath, "BareBoneSolves");
        CreateFolder(BBSolvesPath);

        advanceSolvesPath = Path.Combine(allSolvesPath, "AdvanceSolves");
        CreateFolder(advanceSolvesPath);

        CreateFolder(Path.Combine(roamingPath, appName));
        RoamingProjectPath = Path.Combine(roamingPath, appName);

        settingsPath = Path.Combine(RoamingProjectPath, "Settings.json");
        CreateFile(settingsPath);
    }

    static void CreateFolder(string path)
    {
        if (!Path.Exists(path))
        {
            Directory.CreateDirectory(path);
            Settings.UserData["NewUser"] = "true";
        }
    }

    static void CreateFile(string path)
    {
        File.WriteAllText(path, "");
    }

    public static void SaveBBSolve(BBSolveData solve)
    {
        var options = new JsonSerializerOptions
        {
            WriteIndented = true
        };

        string json = JsonSerializer.Serialize(solve, options);

        if (!Path.Exists(LocalProjectPath))
        {
            MainMenu.PrintError("Project Path Doesn't Exist", "The Local Project's path does not exist. Solve not saved.");
            return;
        }

        if (!Path.Exists(BBSolvesPath))
        {
            MainMenu.PrintError("BB Solves Path Doesn' Exist", "The BB Solve's path does not exist. Solve not saved.");
            return;
        }

        string name = "";

        do
        {
            name = Guid.NewGuid().ToString("N");
        } while (guids.Contains(name));
        guids.Add(name);
       
        File.WriteAllText(Path.Combine(BBSolvesPath, name), json);
    }

    public static void UpdateValues()
    {
        string[] BBSolves = Directory.GetFiles(BBSolvesPath, "*.json");
        if (BBSolves.Length == 0)
        {
            Settings.UserData["NewUser"] = "true";
            return;
        }

        foreach (string BBSolvePath in BBSolves)
        {
            guids.Add(Path.GetFileNameWithoutExtension(BBSolvesPath));
            BBSolveData? solve = JsonSerializer.Deserialize<BBSolveData>(BBSolvePath);
        }
    }

    public static void Restart(int errorCode)
    {
        Process.Start(Environment.ProcessPath);
        Environment.Exit(errorCode);
    }
}