using System;
using System.Linq;
using System.Collections.Generic;
namespace S_Cube;

public static class ScrambleGenerator
{
    public static string GenerateScramble()
    {
        Console.Clear();
        char move = 'A';
        string moves = "RLUDFB";
        string avilableMoves = moves;

        List<string> modifiers = new List<string>();
        modifiers = new List<string> { "", "", "", "'", "'", "2", "2" };

        if (Settings.Preferences["Enable Wide Moves"].Value == "true")
            modifiers.Add("w");
        
        if(Settings.Preferences["Enable Slice Moves"].Value == "true")
        {
            moves += "MES";
            avilableMoves = moves;
        }

        string scramble = "";
        for (int i = 0; i <Random.Shared.Next(20, 26); i++)
        {
            move = avilableMoves[Random.Shared.Next(avilableMoves.Length)];

            //Removing unnecessary moves
            avilableMoves.Replace(move.ToString(), "");

            bool sliceMoves = false;
            if ((move == 'M' || move == 'E' || move == 'S') && Settings.Preferences["Enable Wide Moves"].Value == "true")
            {
                sliceMoves = true;
                modifiers.Remove("w");
            }
            
            string modifier = modifiers[Random.Shared.Next(modifiers.Count)];
            scramble += move + modifier + " ";

            avilableMoves = moves;
        }

        return scramble.Trim();
    }
}