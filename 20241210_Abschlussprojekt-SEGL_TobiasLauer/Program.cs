using System.Runtime.CompilerServices;
using System.Collections.Generic;

namespace _20241210_Abschlussprojekt_SEGL_TobiasLauer
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine();
            Console.WriteLine("*** Willkommen beim Spiel ECHOES OF FORTUNE ***");
            Console.WriteLine();
            Console.WriteLine("Dein spannendes 2D-Konsolen-Schatzsuche-Abenteuer.");
            Console.WriteLine();
            Console.WriteLine("Anleitung: Du als Spieler hast das P-Symbol auf dem Spielfeld.\nDu musst den Schatz mit dem $-Symbol finden ohne dabei in eine\nFalle zu tappen. Bewege dein P-Symbol mit den folgenden Tasten:\nw = nach oben, a = nach links, s = nach unten, d = nach rechts.\nErreichst du den Schatz mit dem $-Symbol ohne in eine Falle zu\ntappen hast du das Spiel gewonnen. Wenn du in eine Falle\ntappst hast du das Spiel verloren.");
            Console.WriteLine();
            Console.WriteLine("Dein Schatzsuche-Abenteuer beginnt jetzt. Los geht's!");
            Console.WriteLine();

            Einzelfeld[] raster10x10 = new Einzelfeld[100];

            for (int i = 0; i < raster10x10.Length; i++)
            {
                raster10x10[i] = new Einzelfeld();
            }

            int zaehlerSpielfeld = 0;
            bool farbe = false;

            foreach (Einzelfeld feld in raster10x10)
            {
                if (zaehlerSpielfeld == 10)
                {
                    Console.WriteLine();
                    zaehlerSpielfeld = 0;

                    if (farbe) farbe = false;
                    else farbe = true;
                }

                feld.EinzelfeldFarbe(farbe);
                zaehlerSpielfeld++;

                if (farbe) farbe = false;
                else farbe = true;
            }












            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("So bewegst du dich auf dem Spielfeld:");
            Console.WriteLine();
            Console.WriteLine("w = nach oben, a = nach links, s = nach unten, d = nach rechts.");
            Console.WriteLine();
            Console.WriteLine();

            Console.ResetColor();
            
            
        }
    }
}
