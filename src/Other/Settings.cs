using System;
using System.Linq;
using System.Collections.Generic;

public static class Settings
{
    public static List<Preference> Preferences = new List<Preference>
    {
        //{"", new Preference("", "", "", "")}
        new Preference("Default Algorithm", "string", "", "The default algorithm used when completing an Advance Solve."),
        new Preference("Add Wide Moves", "bool", "false", "Enables wide moves (such as 'r' or 'rw') to be chosen when generating a scramble."),
        new Preference("Inspection", "bool", "true", "Enables a 15sec inspection time before starting the timer. When inspecting, you aren't allowed to make move. You try to think of moves you would play during the solve.")
    };

    public static List<Preference> AdvancePreferences = new List<Preference>
    {
        new Preference("NewUser", "bool", "false", "Acts as if you are a new user. Turns off after you close S-Cube.")
    };

    public static void Home()
    {
        while(true)
        {
            Console.Clear();
            Console.WriteLine("S-Cube \nSETTINGS /n");

            Console.WriteLine("0. Exit");
            for (int i = 0; i < Preferences.Count; i++)
            {
                Preference preference = Preferences[i];
                Console.WriteLine($"{i + 1}. {preference.Name}: {preference.Value}");
                Console.WriteLine(preference.Info);
            }

            Console.WriteLine("\nAdvance preference:");
            for (int i = 0; i < AdvancePreferences.Count; i++)
            {
                Preference preference = AdvancePreferences[i];
                Console.WriteLine($"{i + 1}. {preference.Name}: {preference.Value}");
                Console.WriteLine(preference.Info);
            }
            Console.WriteLine();

            string input = MainMenu.GetNumber(false, Preferences.Count);

            if (input.StartsWith("Error"))
                continue;

            int number = int.Parse(input);

            if (number == 0)
                return;
            
            //
        }
    }
}

public class Preference
{
    public readonly string Name;
    public readonly string Type;
    public string Value;
    public readonly string Info;

    public Preference(string name, string type, string value, string info)
    {
        Name = name;
        Type = type;
        Value = value;
        Info = info;
    }
}