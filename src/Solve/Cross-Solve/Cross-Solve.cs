using System;
using System.Threading;
using System.Diagnostics;
using System.Collections.Generic;
namespace S_Cube;

public static class CrossSolves
{
    public static bool WriteSolve = ((BoolPr)Settings.Preferences["Write Solve"]).Value;
    public static List<string> Cubes = new List<string>();
    
    public static void Inspection()
    {
        int inspectionDuration = ((IntPr)Settings.Preferences["Inspection Duration"]).Value;
        Stopwatch inspection = new Stopwatch();
        inspection.Start();

        while (inspection.Elapsed.Seconds < inspectionDuration)
        {
            Console.Clear();

            Console.WriteLine("INSPECTION");
            Console.WriteLine("(You can turn this off in the settings)");

            if (!((BoolPr)Settings.Preferences["Show Inspection Duration"]).Value)
                Console.WriteLine("Inspection Time");

            else
                Console.WriteLine($"{inspectionDuration - inspection.Elapsed.Seconds} seconds remaining");
                
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

        Wait(1000);
        char key = char.ToUpperInvariant(Console.ReadKey(true).KeyChar);

        return key == 'Y';
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

    public static void DoSolve(bool advanceSolve, string scramble = "No Scramble Provided")
    {
        Console.Clear();

        if (((BoolPr)Settings.Preferences["Inspection"]).Value)
                Inspection();

        bool showTime = ((BoolPr)Settings.Preferences["Show Time"]).Value;

        DateTime currentDate = DateTime.Now;

        //-Timer stuff
        Stopwatch time = new Stopwatch();
        time = Stopwatch.StartNew();

        MainMenu.Stats.TimeSpentSolving.Start();

        if (advanceSolve)
            MainMenu.Stats.AdvanceSolvesTimer.Start();

        else
            MainMenu.Stats.BBSolvesTimer.Start();


        if (!showTime)
        {
            Console.WriteLine("Solve");
        }
                
        while (true)
        {
            if (Console.KeyAvailable)
                break;
                    
            if (time.ElapsedMilliseconds % 100 == 0)
            {
                if (showTime)
                {
                    Console.Clear();
                    Console.WriteLine("Time: " + time.Elapsed.ToString(@"mm\:ss\.ff"));
                }
            }
        }

        FlushInput();
        time.Stop();
        MainMenu.Stats.TimeSpentSolving.Stop();

        if (advanceSolve)
            MainMenu.Stats.AdvanceSolvesTimer.Stop();

        else
            MainMenu.Stats.BBSolvesTimer.Stop();
        
        Console.Clear();

            Console.WriteLine($"Total Time: {time.Elapsed.ToString(@"mm\:ss\.fff")}");
            Console.WriteLine("(Enter 'D' to delete this solve)");
            Console.WriteLine("(Enter '2' to mark thsi solve as +2)");
            Console.WriteLine("(Enter 'F' to mark this solve as Did Not Finish)");
            Console.WriteLine("(Enter any key to continue)");

            CrossSolves.Wait(1000);
            char input = char.ToUpperInvariant(Console.ReadKey(true).KeyChar);

            Penalty penalty = Penalty.None;
            
            if(input == '2')
            {
                penalty = Penalty.Plus2;
            }

            else if(input == 'F')
            {
                penalty = Penalty.DNF;
            }

        if ((input == 'D' && advanceSolve) && ConfirmDeletion())
        {
            SolveData solve = new SolveData(time.Elapsed, scramble, "No Description Provided", currentDate, new LabelData(), penalty);

            if(WriteSolve)
                Saving.WriteSolve(solve);
        }

        if ((input == 'D'  && !advanceSolve) && ConfirmDeletion())
        {
            BBSolveData solve = new BBSolveData(time.Elapsed, currentDate, new LabelData(), penalty);

            if(WriteSolve)
                Saving.WriteBBSolve(solve);
        }

        Wait(1000);
    }
}