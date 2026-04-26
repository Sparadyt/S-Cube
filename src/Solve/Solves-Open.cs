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
            Console.WriteLine($"Cube: {openedSolve.}");
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
        if (openedSolve.Penalty == Penalty.None)
        {
            Console.WriteLine($"Penalty: None");
        }

        else if(openedSolve.Penalty == Penalty.Plus2)
        {
            Console.WriteLine($"Penalty: +2");
        }

        else if (openedSolve.Penalty == Penalty.DNF)
        {
            Console.WriteLine($"Penalty: DNF");
        }

        else
        {
            Console.WriteLine("Penalty: N/A");
        }
    }

    public static void Delete()
    {
        if (ConfirmDeletion())
            SolveData.Solves.Remove(openedSolve);
    }
}