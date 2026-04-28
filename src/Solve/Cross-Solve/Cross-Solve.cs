using System;
using System.Threading;
using System.Diagnostics;
using System.Collections.Generic;
namespace S_Cube;

public static class CrossSolves
{
    public static void Inspection()
    {
        Stopwatch inspection = new Stopwatch();
        inspection.Start();

        while (inspection.Elapsed.Seconds < 15)
        {
            Console.Clear();

            Console.WriteLine("INSPECTION");
            Console.WriteLine("(You can turn this off in the settings)");

            Console.WriteLine(15 - inspection.Elapsed.Seconds);
            Thread.Sleep(1000);
        }

        FlushInput();
        MainMenu.PlaySFX(Path.Combine(Saving.AudioPath, "InspectionStart.mp3"));
    }

    public static bool ConfirmDeletion()
    {
        Console.Clear();
        Console.WriteLine("ARE YOU SURE YOU WANT TO DELETE THIS SOLVE. IT CANNOT BE UNDONE.");  
        Console.WriteLine("Enter 'Y' to to confirm deletion");
        Console.WriteLine("Enter anything else to not delete this solve");

        char key = char.ToUpperInvariant(Console.ReadKey(true).KeyChar);

        if(key == 'Y')
            return true;
        return false;
    }

    public static void FlushInput()
    {
        while(Console.KeyAvailable)
            Console.ReadKey(true);
    }

    public static void Wait(int msTime)
    {
        Thread.Sleep(msTime);
        FlushInput();
    }
}