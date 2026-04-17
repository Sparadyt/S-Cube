using System;
using System.Diagnostics;
namespace S_Cube;

public static class Info
{
    public static Dictionary<string, string> Links = new Dictionary<string, string>
    {
        {"About", "https://docs.google.com/document/d/1HB8RSRs9GolYm0aSgDCusA4xKO6u7jUqhhVu00NluTk/edit?usp=drivesdk"},
        {"GitHub", "https://github.com/Sparadyt/S-Cube"}
    };

    public static List<(string name, Action action)> options = new List<(string, Action)>
    {
        ("Show Links", ShowLinks),
        ("Contacts", ShowContacts),
        ("Credit", ShowCredits)
    };

    static string[] keys = Links.Keys.ToArray();

    public static void Home()
    {
        ShowLinks();
    }

    public static void ShowLinks()
    {
        Console.Clear();
        Console.WriteLine("S-Cube \nInfo \n");
        string letters = "ABCDEFGHIJKLMNOPQRSTUVWXYX";

        for (int i = 0; i < keys.Length; i++)
        {
            Console.WriteLine($"{keys[i]}:");
            Console.WriteLine($"{letters[i]}: {Links[keys[i]]}");
            Console.WriteLine();
        }

        Console.WriteLine("Enter the indicated charactor to open that link to open the link");
        Console.WriteLine("Enter 'Esc' to exit");

        ConsoleKeyInfo key = Console.ReadKey();

        if (key.Key == ConsoleKey.Escape)
            return;

        if (letters.IndexOf(char.ToUpperInvariant(key.KeyChar)) > keys.Length)
        {
            MainMenu.PrintError("Invalid Input", "Please enter a valid input.");
            return;
        }
        
        for (int i = 0; i < keys.Length; i++)
        {
            if (char.ToUpperInvariant(key.KeyChar) == letters[i])
                Process.Start(new ProcessStartInfo(Links[keys[i]]) { UseShellExecute = true });
        }
    }

    public static void ShowContacts()
    {
        Console.WriteLine("WIP");
        Console.ReadKey();
    }

    public static void ShowCredits()
    {
        Console.WriteLine("WIP");
        Console.ReadKey();
    }
}

//You can use this for mods or anything else too
public record ContactData
{
    public static List<ContactData> Contacts { get; private set; } = new List<ContactData>();
    public readonly string? ContactInfoOf;
    public readonly string Website;
    public readonly string Link;
    public readonly string Info;

    public ContactData(string? contactInfoOf, string website, string link, string info)
    {
        if (string.IsNullOrWhiteSpace(contactInfoOf))
        {
            contactInfoOf = "Anonymous";
        }

        ContactInfoOf = contactInfoOf;
        Website = website;
        Link = link;
        Info = info;
        
        Contacts.Add(this);
    }
}

//You can give yourself credit if you made a mod
public record CreditData
{
    //WIP
}