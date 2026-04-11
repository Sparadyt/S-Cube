using System;
using System.Collections.Generic;

public static partial class Solves
{
    static bool exit = false;
    static void Open()
    {
        exit = false;

        while (true)
        {
            Console.Clear();
            Console.WriteLine("S-Cube \nOPEN SOLVE\n");

            //Printing Info abot the solve
            Console.WriteLine($"Solve Number: {openedSolve.Number}");
            Console.WriteLine($"Time: {openedSolve.Time.ToString(@"mm\:ss\.fff")}");
            Console.WriteLine($"Description: {openedSolve.Description}");
            Console.WriteLine($"Scramble: {openedSolve.Scramble}");
            Console.WriteLine();

            Console.WriteLine($"Date: {openedSolve.Date}");
            ShowUsedAlgorithm();
            Console.WriteLine($"Solves Folder: {openedSolve.SolvesFolder}");

            if (openedSolve.Laps.Count != 0)
            {
                Console.WriteLine("\nLaps:");
            }
            
            for (int i = 0; i < openedSolve.Laps.Count; i++)
            {
                Console.WriteLine($"{openedSolve.Laps[i].Name}: {openedSolve.Laps[i].Time?.ToString(@"mm\:ss\.fff")}");
            }
            Console.WriteLine();

            Console.WriteLine("Enter 'Des' to change the Description");
            //Console.WriteLine("Enter 'Sol' to change the Solves Folder");
            Console.WriteLine("Enter 'Lap' to change the laps' name");
            Console.WriteLine("Enter anyting else to Exit");
            Console.WriteLine();

            HandleInput();

            if(exit)
                break;
        }
    }

    static void ShowUsedAlgorithm()
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

    static void HandleInput()
    {
        string? input = MainMenu.GetString("Enter an input", true);

        if (input == "des")
        {
            ChangeDescription();
        }

        //else if(input == "sol")
        //{
        //ChangeSolveFolder();
        //}

        else if (input == "lap")
        {
            ChangeLapName();
        }

        else
            exit = true;
    }

    static void ChangeDescription()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("S-Cube \nCHANGE DESCRIPTION\n");
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

    static void ChangeSolveFolder()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("S-Cube \nCHANGE SOLVE FOLDER\n");
            string? newFolder = MainMenu.GetString("Enter the new folder's name", false);

            if (newFolder.StartsWith("Error"))
                continue;

            openedSolve.SolvesFolder = Path.Combine(Saving.LocalProjectPath, newFolder);;
            return;
        }
    }

    static void ChangeLapName()
    {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("S-Cube \nCHANGE LAP NAME\n");
                Console.WriteLine("Enter the number of the lap you want the name to change");

                Console.WriteLine("0. Exit");
                for (int i = 0; i < laps.Count; i++)
                {
                    Console.WriteLine($"{i + 1}. {laps[openedSolve.Number - 1][i].Name}: {laps[openedSolve.Number - 1][i].Time?.ToString(@"mm\:ss\:ff")}");
                }
                
                string? input = MainMenu.GetNumber(false, openedSolve.Laps.Count);
    
                if (input.StartsWith("Error"))
                {
                    continue;
                }

                if(input == "0")
                {
                return;
                }
    
                int lapNumber = Convert.ToInt32(input);
    
                Console.WriteLine("Enter the new name of the lap");
                string? newName = MainMenu.GetString("Enter the new name of the lap", false);
    
                if (newName.StartsWith("Error"))
                {
                    continue;
                }
    
                openedSolve.Laps[lapNumber].Name = newName;
            }
    }
}