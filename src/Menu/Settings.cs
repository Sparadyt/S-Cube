using System;
using System.Linq;
using System.Threading;
using System.Collections.Generic;
using System.Text.Json.Serialization;
namespace S_Cube;

public static class Settings
{
    public static Dictionary<string, Preference> Preferences = new Dictionary<string, Preference>
    {
        //{"", new Preference("", "", "", "")}
        {"Default Algorithm", new Preference("Default Algorithm", "string", "", "The default algorithm used when completing an Advance Solve.") },
        {"Enable Wide Moves", new Preference("Enable Wide Moves", "bool", "false", "Enables wide moves (such as 'r'/'rw') to be chosen when generating a scramble.")},
        {"Enable Slice Moves", new Preference("Enable Slice Moves", "bool", "false", "Enables slice moves ('M', 'E', 'S') to be chosen when generating a scramble.") },
        {"Inspection", new Preference("Inspection", "bool", "true", "Enables a 15sec inspection time before starting the timer. When inspecting, you aren't allowed to make move. You try to think of moves you would play during the solve.")},
        {"Ms Interval", new Preference("Ms Interval", "int", "1000", "Millisecond interval in which your stats get saved. Keeping it too low may cause lag.")}
    };

    public static readonly string[] PreferencesNames = Preferences.Keys.ToArray();
    public static void Home()
    {
        while(true)
        {
            Console.Clear();
            Saving.UpdatePreference();
            Console.WriteLine("S-Cube \nSETTINGS /n");

            Console.WriteLine("0. Exit");
            PrintPreferences();

            string input = MainMenu.GetNumber(false, Preferences.Count);

            if (input.StartsWith("Error"))
                continue;

            int number = int.Parse(input);

            if (number == 0)
                return;
            
            Preference pickedPreference = Preferences[GetPreference(number - 1)];

            if (pickedPreference.Type == "bool")
            {
                pickedPreference = BoolPreference(number - 1);
            }

            else if (pickedPreference.Type == "string")
            {
                pickedPreference = StringPreference(number - 1);
            }

            Saving.SavePreferences(Preferences);
        }
    }

    public static string GetPreference(int index)
    {
        if (index <= Preferences.Count)
            return PreferencesNames[index];

        MainMenu.PrintError("Preference Error", "A preference of that index does not exist.", true);
        Saving.Restart(3);
        return "Error";
    }

    static void PrintPreferences()
    {
        for (int i = 0; i < Preferences.Count; i++)
        {
            Preference preference = Preferences[GetPreference(i)];

            Console.Write($"{i + 1}. {preference.Name}: ");

            //Bool
            if (preference.Type == "bool")
            {
                if (preference.Value == "true")
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("ON");
                    Console.ResetColor();
                }

                if (preference.Value == "false")
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("OFF");
                    Console.ResetColor();
                }
            }

            //String
            else if (preference.Type == "string")
            {
                Console.Write("\"");
                Console.Write(preference.Value);
                Console.WriteLine("\"");
            }

            else
            {
                Console.WriteLine(preference.Value);
            }
        }
    }

    static Preference BoolPreference(int index)
    {
        Preference preference = Preferences[GetPreference(index)];

        while (true)
        {
            Console.Clear();

            Console.Write($"{preference.Name}: ");
            if (preference.Value == "true")
                Console.WriteLine("On");

            else if (preference.Value == "false")
                Console.WriteLine("Off");

            else
            {
                MainMenu.PrintError("Invalid Value", "Invalid value for a bool preference", true);
                return preference;
            }

            Console.WriteLine(preference.Info);
            Console.WriteLine();

            Console.WriteLine("Enter 'O' to turn this on");
            Console.WriteLine("Enter 'F' to turn this off");
            Console.WriteLine("(Enter anything else to exit)");
            Console.Write("> ");

            ConsoleKeyInfo key = Console.ReadKey(true);

            if (Char.ToUpperInvariant(key.KeyChar) == 'O')
            {
                preference.Value = "true";
                Console.WriteLine("Preference turned ON sucessfully!");
            }

            else if (Char.ToUpperInvariant(key.KeyChar) == 'F')
            {
                preference.Value = "false";
                Console.WriteLine("Preference turned OFF sucessfully!");
            }

            else
                break;

            Thread.Sleep(1000);
            break;
        }

        return preference;
    }

    static Preference StringPreference(int index)
    {
        Preference preference = Preferences[GetPreference(index)];

        while (true)
        {
            Console.Clear();

            Console.WriteLine($"{preference.Name}: \"{preference.Value}\"");
            Console.WriteLine(preference.Info);
            Console.WriteLine();

            Console.WriteLine("Enter a new value for this preference");
            Console.WriteLine("Enter \"Esc\" to exit");
            Console.Write("> ");

            string input = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(input))
            {
                MainMenu.PrintError("Invalid Input", "Please enter something.");
                continue;
            }

            if((input.ToLower()) == "esc")
                break;
            
            preference.Value = input;
            Console.WriteLine("Preference updated sucessfully!");
            Thread.Sleep(1000);
            break;
        }

        return preference;
    }
}

public class Preference
{
    public string Name {get; set;}
    public string Type { get; set; }
    public string Value {get; set;}
    public string Info{ get; set; }

    [JsonConstructor]
    public Preference(string name, string type, string value, string info)
    {
        Name = name;
        Type = type;
        Value = value;
        Info = info;
    }
}