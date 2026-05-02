using System;
using System.Collections.Generic;
namespace S_Cube;

public static partial class SeeStats
{
    public static void Home()
    {
        Console.Clear();
        Console.WriteLine($"{Saving.AppName} \nSTATS \n");
        Console.WriteLine("Cubes: ");

        if(CrossSolves.Cubes.Count == 1)
        {
            Algorithm(CrossSolves.Cubes[0]);
        }

        else
        {
            while (true)
            {
                for (int i = 0; i < CrossSolves.Cubes.Count; i++)
                {
                    Console.WriteLine($"{i + 1}. {CrossSolves.Cubes[i]}");
                }

                string input = MainMenu.GetNumber(false, CrossSolves.Cubes.Count - 1);

                if (input.StartsWith("Error"))
                    break;

                int number = int.Parse(input);

                Algorithm(CrossSolves.Cubes[number - 1]);
            }
        }
    }

    public static void Algorithm(string cube)
    {
        //
    }
}