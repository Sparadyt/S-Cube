using System;
using System.IO;
using System.Text.Json;
using System.Diagnostics;
using System.Collections.Generic;
namespace S_Cube;

public static class Saving
{
    static List<string> bbGuids = new List<string>();
    public static List<string> Guids = new List<string>();
    public static string ProjectDir = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, @"..\..\..\..\"));

    public static string AudioPath = Path.Combine(ProjectDir, Path.Combine("Assets", "Audio"));
    public static string? LocalProjectPath { get; private set; } = "";
    public static string? RoamingProjectPath { get; private set; } = "";
    public static string ModsPath = "";
    public static string AppName = "S-Cube";
    public static string? BBSolvesPath = "";
    public static string? SolvesPath = "";
    public static string? SettingsPath = "";
    public static string? StatsPath = "";

    public static void CreateFiles()
    {
        string localPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
        string roamingPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);

        CreateFolder(Path.Combine(localPath, AppName));
        LocalProjectPath = Path.Combine(localPath, AppName);

        string allSolvesPath = Path.Combine(LocalProjectPath, "AllSolves");
        CreateFolder(allSolvesPath);

        BBSolvesPath = Path.Combine(allSolvesPath, "BareBoneSolves");
        CreateFolder(BBSolvesPath);

        SolvesPath = Path.Combine(allSolvesPath, "AdvanceSolves");
        CreateFolder(SolvesPath);

        CreateFolder(Path.Combine(roamingPath, AppName));
        RoamingProjectPath = Path.Combine(roamingPath, AppName);

        ModsPath = Path.Combine(LocalProjectPath, "Mods");
        CreateFolder(ModsPath);

        SettingsPath = Path.Combine(LocalProjectPath, "Settings.json");
        CreateFile(SettingsPath);

        StatsPath = Path.Combine(LocalProjectPath, "Stats.json");
        CreateFile(StatsPath);
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

    public static void WriteBBSolve(BBSolveData solve)
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

        if (!Path.Exists(BBSolvesPath))
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
        
        solve.Path = name + ".json";
        File.WriteAllText(Path.Combine(BBSolvesPath, name + ".json"), json);
    }

    public static void WriteSolve(SolveData solve)
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
        } while (Guids.Contains(name));
        Guids.Add(name);

        solve.Path = name + ".json";
        File.WriteAllText(Path.Combine(SolvesPath, name + ".json"), json);
    }

    public static void UpdateValues()
    {
        //Adding saved solves
        string[] BBSolves = Directory.GetFiles(BBSolvesPath, "*.json");
        if (BBSolves.Length == 0)
        {
            MainMenu.Stats.NewUser = true;
        }

        foreach (string bbSolvePath in BBSolves)
        {
            bbGuids.Add(Path.GetFileNameWithoutExtension(bbSolvePath));
            BBSolveData? solve = JsonSerializer.Deserialize<BBSolveData>(File.ReadAllText(bbSolvePath));

            if (solve.IsUnderChosenSeconds(10))
                MainMenu.Stats.SolvesUnder10Second++;

            if (solve.IsUnderChosenSeconds(30))
                MainMenu.Stats.SolvesUnder30Second++;

            solve.Path = bbSolvePath;

            bool practice = (solve.Labels ?? new LabelData()).Practice ?? false;

            if(!practice)
                CrossSolves.Cubes.Add(solve.Labels.Cube);
        }

        string[] solves = Directory.GetFiles(SolvesPath, "*.json");
        if (solves.Length == 0)
        {
            MainMenu.Stats.NewUser = true;
            return;
        }

        foreach (string solvePath in solves)
        {
            Guids.Add(Path.GetFileNameWithoutExtension(SolvesPath));

            SolveData? solve = JsonSerializer.Deserialize<SolveData>(File.ReadAllText(solvePath));

            if (solve.IsUnderChosenSeconds(10))
                MainMenu.Stats.SolvesUnder10Second++;

            else if (solve.IsUnderChosenSeconds(30))
                MainMenu.Stats.SolvesUnder30Second++;

            solve.Path = solvePath;

             if(!(bool)solve.Labels.Practice)
                CrossSolves.Cubes.Add(solve.Labels.Cube);
        }
    }

    public static void UpdateStats()
    {
        string json = File.ReadAllText(StatsPath);

        if (!IsValidJson(json, StatsPath, true))
        {
            SaveStats(MainMenu.Stats);
            return;
        }
            
        MainMenu.Stats = JsonSerializer.Deserialize<StatsData>(json) ?? new StatsData();
    }


    public static void UpdatePreference()
    {
        var options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };

        string json = File.ReadAllText(SettingsPath);

        if (!IsValidJson(json, SettingsPath, true))
        {
            SavePreferences(Settings.Preferences);
            return;
        }
        
        Settings.Preferences = JsonSerializer.Deserialize<Dictionary<string, Preference>>(json, options) ?? new Dictionary<string, Preference>();
    }

    public static void SaveStats(StatsData stats)
    {
        var options = new JsonSerializerOptions
        {
            WriteIndented = true
        };

        string json = JsonSerializer.Serialize(stats, options);

        if (!Path.Exists(StatsPath))
        {
            CreateFiles();
            MainMenu.PrintError("Stats' Path Does Not Exist", "The Stats' path does not exist. Stats saved. (Did you delete it?)");
        }
        
        File.WriteAllText(StatsPath, json);
    }

    public static void SavePreferences(Dictionary<string, Preference> preferences)
    {
        var options = new JsonSerializerOptions
        {
            WriteIndented = true
        };

        string json = JsonSerializer.Serialize(preferences, options);

        if (!Path.Exists(SettingsPath))
        {
            CreateFiles();
            MainMenu.PrintError("Settings' Path Does Not Exist", "The Settings' path does not exist. Preferencees saved. (Did you delete it?)");
        }
        
        File.WriteAllText(SettingsPath, json);
    }

    public static void Restart(int errorCode)
    {
        Process.Start(Environment.ProcessPath);
        Environment.Exit(errorCode);
    }

    public static void Recompile()
    {
        Process.Start(new ProcessStartInfo("dotnet", "run")
        {
            WorkingDirectory = Path.Combine(ProjectDir, @"..\"),
            UseShellExecute = true
        });

        //Exit Code 4 means restart
        Environment.Exit(4);
    }

    public static bool IsValidJson(string json, string fileName, bool giveError)
    {
        if(string.IsNullOrWhiteSpace(json))
            return false;

        try
        {
            using (JsonDocument.Parse(json))
                return true;
        }

        catch(JsonException)
        {
            if(giveError)
                MainMenu.PrintError("Invalid JSON", $"The {fileName} file was invalid. You could have changed it.", true);
        }

        return false;
    }
}