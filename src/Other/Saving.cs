using System;
using System.IO;
using System.Text.Json;
using System.Diagnostics;
using System.Collections.Generic;
namespace S_Cube;

public static class Saving
{
    static List<string> bbGuids = new List<string>();
    static List<string> guids = new List<string>();
    public static string? LocalProjectPath { get; private set; } = "";
    public static string? RoamingProjectPath { get; private set; } = "";
    public static string appName = "S-Cube";
    public static string? bbSolvesPath = "";
    public static string? SolvesPath = "";
    public static string? settingsPath = "";
    public static string? statsPath = "";

    public static void CreateFiles()
    {
        string localPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
        string roamingPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);

        CreateFolder(Path.Combine(localPath, appName));
        LocalProjectPath = Path.Combine(localPath, appName);

        string allSolvesPath = Path.Combine(LocalProjectPath, "AllSolves");
        CreateFolder(allSolvesPath);

        bbSolvesPath = Path.Combine(allSolvesPath, "BareBoneSolves");
        CreateFolder(bbSolvesPath);

        SolvesPath = Path.Combine(allSolvesPath, "AdvanceSolves");
        CreateFolder(SolvesPath);

        CreateFolder(Path.Combine(roamingPath, appName));
        RoamingProjectPath = Path.Combine(roamingPath, appName);

        settingsPath = Path.Combine(LocalProjectPath, "Settings.json");
        CreateFile(settingsPath);

        statsPath = Path.Combine(LocalProjectPath, "Stats.json");
        CreateFile(statsPath);
    }

    public static void CreateFolder(string path)
    {
        if (!Path.Exists(path))
        {
            Directory.CreateDirectory(path);
            MainMenu.Stats.NewUser = true;
        }
    }

    public static void CreateFile(string path)
    {
        if(!File.Exists(path))
            File.Create(path).Close();
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
            CreateFiles();
            MainMenu.PrintError("Project Path Doesn't Exist", "The Local Project's path does not exist. Solve saved. (Did you delete it?).");
        }

        if (!Path.Exists(bbSolvesPath))
        {
            CreateFiles();
            MainMenu.PrintError("BB Solves Path Doesn' Exist", "The BB Solve's path does not exist. Solve saved. (Did you delete it?).");
        }

        string name = "";

        do
        {
            name = Guid.NewGuid().ToString("N");
        } while (bbGuids.Contains(name));
        bbGuids.Add(name);
       
        File.WriteAllText(Path.Combine(bbSolvesPath, name + ".json"), json);
    }

    public static void SaveSolve(SolveData solve)
    {
        var options = new JsonSerializerOptions
        {
            WriteIndented = true
        };

        string json = JsonSerializer.Serialize(solve, options);

        if (!Path.Exists(LocalProjectPath))
        {
            CreateFiles();
            MainMenu.PrintError("Project Path Doesn't Exist", "The Local Project's path does not exist. Solve saved.(Did you delete it?).");
    }

        if (!Path.Exists(SolvesPath))
        {
            CreateFiles();
            MainMenu.PrintError("Solves Path Doesn' Exist", "The Solve's path does not exist. Solve saved. (Did you delete it?).");
        }

        string name = "";

        do
        {
            name = Guid.NewGuid().ToString("N");
        } while (guids.Contains(name));
        guids.Add(name);
       
        File.WriteAllText(Path.Combine(SolvesPath, Path.Combine(name, ".json")), json);
    }

    public static void UpdateValues()
    {
        //Adding saved solves
        string[] BBSolves = Directory.GetFiles(bbSolvesPath, "*.json");
        if (BBSolves.Length == 0)
        {
            MainMenu.Stats.NewUser = true;
        }

        foreach (string BBSolvePath in BBSolves)
        {
            Console.WriteLine(BBSolvePath);
            bbGuids.Add(Path.GetFileNameWithoutExtension(bbSolvesPath));
            BBSolveData? solve = JsonSerializer.Deserialize<BBSolveData>(File.ReadAllText(BBSolvePath));

            if (solve.IsUnderChosenSeconds(30))
                MainMenu.Stats.SolvesUnder30Second++;

            else if (solve.IsUnderChosenSeconds(10))
                MainMenu.Stats.SolvesUnder10Second++;
        }

        string[] solves = Directory.GetFiles(SolvesPath, "*.json");
        if (solves.Length == 0)
        {
            MainMenu.Stats.NewUser = true;
            return;
        }

        foreach (string solvePath in solves)
        {
            guids.Add(Path.GetFileNameWithoutExtension(SolvesPath));
            SolveData? solve = JsonSerializer.Deserialize<SolveData>(File.ReadAllText(solvePath));

            if (solve.IsUnderChosenSeconds(30))
                MainMenu.Stats.SolvesUnder30Second++;

            else if (solve.IsUnderChosenSeconds(10))
                MainMenu.Stats.SolvesUnder10Second++;
        }
    }

    public static void UpdateStats()
    {
        string json = File.ReadAllText(statsPath);

        if (string.IsNullOrWhiteSpace(json))
            json = "{ }";
            
        MainMenu.Stats = JsonSerializer.Deserialize<StatsData>(json) ?? new StatsData();
    }


    public static void UpdatePreference()
    {
        var options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };

        string json = File.ReadAllText(settingsPath);
        Settings.Preferences = JsonSerializer.Deserialize<Dictionary<string, Preference>>(json, options) ?? new Dictionary<string, Preference>();
    }

    public static void SaveStats(StatsData stats)
    {
        var options = new JsonSerializerOptions
        {
            WriteIndented = true
        };

        string json = JsonSerializer.Serialize(stats, options);

        if (!Path.Exists(statsPath))
        {
            CreateFiles();
            MainMenu.PrintError("Stats' Path Does Not Exist", "The Stats' path does not exist. Stats saved. (Did you delete it?)");
        }
        
        File.WriteAllText(statsPath, json);
    }

    public static void SavePreferences(Dictionary<string, Preference> preferences)
    {
        var options = new JsonSerializerOptions
        {
            WriteIndented = true
        };

        string json = JsonSerializer.Serialize(preferences, options);

        if (!Path.Exists(settingsPath))
        {
            CreateFiles();
            MainMenu.PrintError("Settings' Path Does Not Exist", "The Settings' path does not exist. Preferencees saved. (Did you delete it?)");
        }
        
        File.WriteAllText(settingsPath, json);
    }

    public static void Restart(int errorCode)
    {
        Process.Start(Environment.ProcessPath);
        Environment.Exit(errorCode);
    }
}