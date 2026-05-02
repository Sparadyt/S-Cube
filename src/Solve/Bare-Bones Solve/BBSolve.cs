using System;
using System.IO;
using System.Linq;
using System.Diagnostics;
using System.Collections.Generic;
namespace S_Cube;

public static class BBSolves
{
    static BBSolveData openedSolve;

    public static void Home()
    {
        (string option, Action action)[] options =
        {
            ("Exit", null),
            ("Do Solve", Do)
        };

        while (true)
        {
            Console.Clear();
            Console.WriteLine($"{Saving.AppName} \nBare-Bones SOLVES \n");

            Console.WriteLine($"Average Time: {BBSolveData.Mean.ToString(@"mm\:ss\.fff")}\n");
            for (int i = 0; i < options.Length; i++)
            {
                Console.WriteLine($"{i}. {options[i].option}");
            }

            for (int i = options.Length - 1; i < BBSolveData.Solves.Count + options.Length - 1; i++)
            {
                Console.WriteLine($"{i + 1}. Solve Number: {BBSolveData.Solves[i - options.Length + 1].Number}");
            }

            Console.WriteLine($"\n(Enter {options.Length} to view Solve number 1)");
            string inputStr = MainMenu.GetNumber(false, BBSolveData.Amount + options.Length - 1);


            if (inputStr.StartsWith("Error"))
                continue;

            int input = int.Parse(inputStr);

            if (input == 0)
                return;
                
            if (input < options.Length)
            {
                options[input].action();
                continue;
            }

            openedSolve = BBSolveData.Solves[input - options.Length];
            Open();
        }
    }

    public static void Do()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine($"{Saving.AppName} \nDO BARE-BONES SOLVE \n");
            Console.WriteLine($"Scramble: {ScrambleGenerator.GenerateScramble()}");
            Console.WriteLine("Enter 'Esc' or 'E' to Exit");
            Console.WriteLine("Enter any other key to start");

            CrossSolves.Wait(1000);
            ConsoleKeyInfo key = Console.ReadKey(true);

            if (key.Key == ConsoleKey.Escape || char.ToUpperInvariant(key.KeyChar) == 'E')
                return;

            Console.Clear();
            CrossSolves.DoSolve(false);
        }
    }

    static void Open()
    {
        Console.Clear();
        Console.WriteLine($"BB Solve Number: {openedSolve.Number}");
        Console.WriteLine($"Time: {openedSolve.Time.ToString(@"mm\:ss\.fff")}");
        Console.WriteLine($"Date: {openedSolve.Date}");
        Console.WriteLine("(Enter 'D' to delete this solve)");
        Console.WriteLine("(Enter any other key to continue)");
        char key = char.ToUpperInvariant(Console.ReadKey().KeyChar);

        if (key == 'D' && CrossSolves.ConfirmDeletion())
            DeleteSolve(openedSolve);
    }

    public static void DeleteSolve(BBSolveData solve)
    {
        BBSolveData.Solves.Clear();

        if (Path.Exists(solve.Path))
            File.Delete(solve.Path);
            
        Saving.UpdateValues();
    }
}