using System;
using System.Diagnostics;

public static class About
{
    public static void Home()
    {
        Console.Clear();
        Console.WriteLine("Paste this link in a Search Engine");
        Console.WriteLine(@"https://docs.google.com/document/d/1HB8RSRs9GolYm0aSgDCusA4xKO6u7jUqhhVu00NluTk/edit?usp=drivesdk");
        Console.WriteLine("Enter 'E' to open the link");
        Console.WriteLine("Enter anything else to exit");

        ConsoleKeyInfo key = Console.ReadKey();

        if (char.ToUpperInvariant(key.KeyChar) == 'E')
            Process.Start(new ProcessStartInfo(@"https://docs.google.com/document/d/1HB8RSRs9GolYm0aSgDCusA4xKO6u7jUqhhVu00NluTk/edit?usp=drivesdk") { UseShellExecute = true });
    }
}