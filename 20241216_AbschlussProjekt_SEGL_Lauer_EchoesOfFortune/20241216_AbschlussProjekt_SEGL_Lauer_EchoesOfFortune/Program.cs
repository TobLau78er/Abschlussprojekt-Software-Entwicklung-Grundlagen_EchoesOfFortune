﻿using System.ComponentModel.Design;

namespace _20241216_AbschlussProjekt_SEGL_Lauer_EchoesOfFortune
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\r\n   _    _____     _                              __   _____          _                       _  \r\n  | |  | ____|___| |__   ___   ___  ___    ___  / _| |  ___|__  _ __| |_ _   _ _ __   ___   | | \r\n / __) |  _| / __| '_ \\ / _ \\ / _ \\/ __|  / _ \\| |_  | |_ / _ \\| '__| __| | | | '_ \\ / _ \\ / __)\r\n \\__ \\ | |__| (__| | | | (_) |  __/\\__ \\ | (_) |  _| |  _| (_) | |  | |_| |_| | | | |  __/ \\__ \\\r\n (   / |_____\\___|_| |_|\\___/ \\___||___/  \\___/|_|   |_|  \\___/|_|   \\__|\\__,_|_| |_|\\___| (   /\r\n  |_|                                                                                       |_| \r\n");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("*** Willkommen beim Spiel ECHOES OF FORTUNE ***");
            Console.WriteLine();
            Console.WriteLine("Dein spannendes 2D-Konsolen-Schatzsuche-Abenteuer.");
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Anleitung:");
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("Als Spieler hast das P-Symbol auf dem Spielfeld.\nDu musst den Schatz mit dem $-Symbol finden ohne\ndabei in eine Falle zu tappen. Bewege dein\nP-Symbol mit den folgenden Tasten: w = nach oben,\na = nach links, s = nach unten, d = nach rechts.\nErreichst du den Schatz mit dem $-Symbol ohne in\neine Falle zu tappen hast du das Spiel gewonnen.\nWenn du in eine Falle tappst hast du das Spiel\nverloren.");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine();
            Console.WriteLine("Dein Schatzsuche-Abenteuer beginnt jetzt. Los geht's!");
            Console.WriteLine();
            Console.ResetColor();

            Einzelfeld[,] spielfeld = new Einzelfeld[10, 10];

            for (int y = 0; y < 10; y++)
            {
                for (int x = 0; x < 10; x++)
                {
                    spielfeld[y, x] = new Einzelfeld
                    {
                        PosZeile = x,
                        PosSpalte = y,
                        HatSpieler = false,
                        HatSchatz = false,
                        HatFalle = false,
                    };
                    spielfeld[y, x].AktualisiereFeld();
                }

            }

            Random zufall = new Random();

            int spielerX = zufall.Next(0, 10);
            int spielerY = zufall.Next(0, 10);

            spielfeld[spielerY, spielerX].HatSpieler = true;
            spielfeld[spielerY, spielerX].AktualisiereFeld();

            int schatzX, schatzY;

            do
            {
                schatzX = zufall.Next(0, 10);
                schatzY = zufall.Next(0, 10);

            } while (schatzX == spielerX && schatzY == spielerY);

            spielfeld[schatzY, schatzX].HatSchatz = true;
            spielfeld[schatzY, schatzX].AktualisiereFeld();

            for (int i = 0; i < 25; i++)
            {

            }
            
            int falleX, falleY;

            do
            {
                falleX = zufall.Next(0, 10);
                falleY = zufall.Next(0, 10);

            } while ((falleX == spielerX && falleY == spielerY) || (falleX == schatzX && falleY == schatzY));

            spielfeld[falleY, falleX].HatFalle = true;
            spielfeld[falleY, falleX].AktualisiereFeld();

            Console.WriteLine();
            Console.ResetColor();
            Console.ReadKey();
            ZeichneSpielfeld(spielfeld, spielerX, spielerY);
            
        }
        static void ZeichneSpielfeld(Einzelfeld[,] spielfeld, int spielerX,int spielerY)
        {
            for (int y = 0; y < 10; y++)
            {
                for (int x = 0; x < 10; x++)
                {
                    Console.Write(spielfeld[y, x].Symbol + " ");
                }
                Console.WriteLine();
            }

            bool spielLaeuft = true;

            while (spielLaeuft)
            {
                Console.Clear();
                //ZeichneSpielfeld();
                Console.WriteLine("Bewebung (w/a/s/d)");
                string eingabe = Console.ReadLine();
                int neuerX = spielerX, neuerY = spielerY;

                if (eingabe == "w") neuerY--;
                else if (eingabe == "a") neuerX--;
                else if (eingabe == "s") neuerY++;
                else if (eingabe == "d") neuerX++;

                if (neuerX >= 0 && neuerX < 10 && neuerY >= 0 && neuerY < 10)
                {
                    spielfeld[spielerY, spielerX].HatSpieler = false;
                    spielfeld[spielerY, spielerX].AktualisiereFeld();

                    spielerX = neuerX;
                    spielerY = neuerY;

                    Einzelfeld neuesFeld = spielfeld[spielerY, spielerX];

                    if (neuesFeld.HatFalle)
                    {
                        Console.WriteLine("Du bist in eine Falle getappt. Du hast das Spiel leider verloren.");
                        spielLaeuft = false;
                    }
                    else if (neuesFeld.HatSchatz)
                    {
                        Console.WriteLine("Gratulation, du hast den wertvollen Schatz gefunden. Du hast das Spiel gewonnen.");
                        spielLaeuft = false;

                    }
                    else
                    {
                        neuesFeld.HatSpieler = true;
                        neuesFeld.AktualisiereFeld();
                    }

                }
                else
                {
                    Console.WriteLine("Ungültige Bewegung!");
                }

                Console.Clear();
                //ZeichneSpielfeld();
                Console.WriteLine("Das Spiel ist nun beendet.");
            }

            Console.WriteLine();
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("So bewegst du dich auf dem Spielfeld:");
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("w = nach oben, a = nach links, s = nach unten, d = nach rechts.");
            Console.WriteLine();
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("*** Copyright: 2024 Tobias Lauer ***");
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
        }
    }
}