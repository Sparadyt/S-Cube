using System;
using System.Linq;
using System.Collections.Generic;

public static class ScrambleGenerator
{
    public static string GenerateScramble()
    {
        Console.Clear();
        char previousMove = '0';
        char move = 'A';
        char[] moves = { 'R', 'L', 'U', 'D', 'F', 'B' };
        List<char> avilableMoves = new List<char>(moves.ToList());

        List<string> modifiers = new List<string>();
        modifiers = ["", "", "", "'", "'", "2", "2"];

        if (Settings.AddWideMoves)
            modifiers.Add("w");

        string scramble = "";
        for (int i = 0; i < MainMenu.rand.Next(20, 26); i++)
        {
            if ((i % 2) == 0)
            {
                avilableMoves = moves.ToList();
            }

            move = avilableMoves[MainMenu.rand.Next(avilableMoves.Count)];

            //Removing unnecessary moves
            if ((Array.IndexOf(moves, move) % 2) == 0)
            {
                avilableMoves.RemoveAt(Array.IndexOf(avilableMoves.ToArray(), move) + 1);
            }

            else
            {
                avilableMoves.RemoveAt(Array.IndexOf(avilableMoves.ToArray(), move) - 1);
            }

            avilableMoves.RemoveAt(Array.IndexOf(avilableMoves.ToArray(), move));

            previousMove = move;
            string modifier = modifiers[MainMenu.rand.Next(modifiers.Count)];
            scramble += move + modifier + " ";
        }

        return scramble.Trim();
    }
}