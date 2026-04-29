using System;
using System.Collections.Generic;
namespace S_Cube;

public static partial class Solves
{
    static bool exit = false;
    static void Open()
    {
        exit = false;

        while (true)
        {
            Console.Clear();
            Console.WriteLine($"{Saving.AppName} \nOPEN SOLVE\n");

            //Printing Info abot the solve
            Console.WriteLine($"Cube: {openedSolve.Labels.Cube}");
            Console.WriteLine($"Solve Number: {openedSolve.Number}");
            Console.WriteLine($"Time: {openedSolve.Time.ToString(@"mm\:ss\.fff")}");
            Console.WriteLine($"Description: {openedSolve.Description}");
            Console.WriteLine($"Scramble: {openedSolve.Scramble}");
            ShowPenalty();
            Console.WriteLine();

            Console.WriteLine($"Date: {openedSolve.Date}");
            ShowUsedAlgorithm();

            Console.WriteLine("Enter 'Des' to change the Description");
            //Console.WriteLine("Enter 'Lab' to change the Labels");
            Console.WriteLine("Enter 'Rem' to delete this solve");
            Console.WriteLine("Enter anyting else to Exit");
            Console.WriteLine();

            HandleInput();

            if(exit)
                break;
        }
    }

    public static void ShowUsedAlgorithm()
    {
        if (openedSolve.UsedAlgorithm != null)
        {
            Console.WriteLine($"Used Algorithm: {openedSolve.UsedAlgorithm}");
        }

        else
        {
            Console.WriteLine("Used Algorithm: N/A");
        }
    }

    public static void HandleInput()
    {
        string? input = MainMenu.GetString("Enter an input", true);

        if (input == "des")
        {
            ChangeDescription();
        }

        else if (input == "rem")
        {
            Delete();
        }

        else if (input == "lab")
        {
            Labels();
        }

        else
            exit = true;
    }

    public static void ChangeDescription()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine($"{Saving.AppName} \nCHANGE DESCRIPTION\n");
            string? newDescription = MainMenu.GetString("Enter the new description", true);

            if (string.IsNullOrWhiteSpace(newDescription))
            {
                newDescription = "No Description Provided";
            }

            if (newDescription.StartsWith("Error"))
            {
                return;
            }

            openedSolve.Description = newDescription;
            return;
        }
    }

    public static void ShowPenalty()
    {
        Console.Clear();
        Console.WriteLine($"{Saving.AppName} \nSHOW PENALTY \n");

        for (int i = 0; i < Enum.GetValues<Penalty>().Length; i++)
        {
            if (openedSolve.Penalty == (Penalty)i)
            {
                Console.WriteLine($"Penalty: {(Penalty)i}");
                break;
            }
        }

        Console.WriteLine("(Enter any key to continue)");
        CrossSolves.Wait(1000);
        Console.ReadKey(true);
    }

    public static void Labels()
    {
        while(true)
        {
            
        }
    }

    public static void Delete()
    {
        if (CrossSolves.ConfirmDeletion())
            SolveData.Solves.Remove(openedSolve);
    }
}