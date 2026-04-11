using System;
using System.Diagnostics;

public static class About
{
    static Dictionary<string, string> links = new Dictionary<string, string>
    {
        {"About", "https://docs.google.com/document/d/1HB8RSRs9GolYm0aSgDCusA4xKO6u7jUqhhVu00NluTk/edit?usp=drivesdk"},
        {"GitHub", "https://github.com/Sparadyt/S-Cube"}
    };

    static string[] keys = links.Keys.ToArray();

    public static void Home()
    {
        Console.Clear();
        string letters = "ABCDEFGHIJKLMNOPQRSTUVWXYX";

        for (int i = 0; i < keys.Length; i++)
        {
            Console.WriteLine($"{keys[i]}:");
            Console.WriteLine($"{i}: {links[keys[i]]}");
        }

        Console.WriteLine("Enter the indicated charactor to open that link to open the link");
        Console.WriteLine("Enter 'Esc' to exit");

        ConsoleKeyInfo key = Console.ReadKey();

        if (key.Key == ConsoleKey.Escape)
            return;
            
        for (int i = 0; i < keys.Length; i++)
        {
            if (char.ToUpperInvariant(key.KeyChar) == letters[i])
                Process.Start(new ProcessStartInfo(links[keys[i]]) { UseShellExecute = true });
        }
    }
}