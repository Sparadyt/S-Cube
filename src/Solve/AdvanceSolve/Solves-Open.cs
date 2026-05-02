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
            Console.WriteLine($"Solve Number: {openedSolve.Number}");
            Console.WriteLine($"Time: {openedSolve.Time.ToString(@"mm\:ss\.fff")}");
            Console.WriteLine($"Description: {openedSolve.Description}");
            Console.WriteLine($"Scramble: {openedSolve.Scramble}");
            ShowPenalty(openedSolve);
            ShowLabels(openedSolve);

            Console.WriteLine($"Date: {openedSolve.Date}");
            Console.WriteLine($"Path: {openedSolve.Path ?? "N/A"}");
            ShowUsedAlgorithm(openedSolve);

            Console.WriteLine();
            Console.WriteLine("Enter 'Des' to change the Description");
            Console.WriteLine("Enter 'Lab' to change the Labels");
            Console.WriteLine("Enter 'Rem' to delete this solve");
            Console.WriteLine("Enter anyting else to Exit");
            Console.WriteLine();

            HandleInput();

            if(exit)
                break;
        }
    }

    public static void ShowUsedAlgorithm(SolveData _openedSolve)
    {
        if (_openedSolve.UsedAlgorithm != null)
        {
            Console.WriteLine($"Used Algorithm: {_openedSolve.UsedAlgorithm}");
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

    public static void ShowPenalty(SolveData _openedSolve)
    {
        for (int i = 0; i < Enum.GetValues<Penalty>().Length; i++)
        {
            if (_openedSolve.Penalty == (Penalty)i)
            {
                Console.WriteLine($"Penalty: {(Penalty)i}");
                break;
            }
        }
    }

    public static void ShowLabels(SolveData _openedSolve)
    {
        if (_openedSolve.Labels == null)
        {
            Console.WriteLine("Labels: N/A");
            return;
        }
        
        Console.WriteLine($"Practice: {_openedSolve.Labels.Practice}");
        Console.WriteLine($"Cube: {_openedSolve.Labels.Cube}");

        Console.WriteLine("Other labels: ");
        foreach (string label in _openedSolve.Labels.OtherLabels)
            Console.WriteLine(label);
            
        Console.WriteLine();
    }

    public static void Delete()
    {
        if (CrossSolves.ConfirmDeletion())
            Solves.DeleteSolve(openedSolve);

        exit = true;
    }
}