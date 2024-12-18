﻿using System.ComponentModel.Design;
using System.Linq.Expressions;
using System.Threading;
using NAudio.Wave;

namespace _20241216_AbschlussProjekt_SEGL_Lauer_EchoesOfFortune
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.BackgroundColor = ConsoleColor.DarkBlue;
            Console.Clear();

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\r\n   _    _____     _                              __   _____          _                       _  \r\n  | |  | ____|___| |__   ___   ___  ___    ___  / _| |  ___|__  _ __| |_ _   _ _ __   ___   | | \r\n / __) |  _| / __| '_ \\ / _ \\ / _ \\/ __|  / _ \\| |_  | |_ / _ \\| '__| __| | | | '_ \\ / _ \\ / __)\r\n \\__ \\ | |__| (__| | | | (_) |  __/\\__ \\ | (_) |  _| |  _| (_) | |  | |_| |_| | | | |  __/ \\__ \\\r\n (   / |_____\\___|_| |_|\\___/ \\___||___/  \\___/|_|   |_|  \\___/|_|   \\__|\\__,_|_| |_|\\___| (   /\r\n  |_|                                                                                       |_| \r\n");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("*** Willkommen beim Spiel ECHOES OF FORTUNE ***");
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("************************************************************************************************");
            Console.WriteLine();
            string musikPfad = "C:\\Users\\LauerTobias\\source\\repos\\20241216_AbschlussProjekt_SEGL_Lauer_EchoesOfFortune\\musik.wav";
            Thread musikThread = new Thread(() => SpieleMusik(musikPfad));
            musikThread.IsBackground = true;
            musikThread.Start();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Dein spannendes 2D-Konsolen-Schatzsuche-Abenteuer.");
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Anleitung:");
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("Als Spieler hast du das P-Symbol auf dem Spielfeld.\nDu musst den Schatz mit dem $-Symbol finden ohne\ndabei in eine Falle zu tappen. Bewege dein\nP-Symbol mit den folgenden Tasten: W = nach oben,\nA = nach links, S = nach unten, D = nach rechts.\nErreichst du den Schatz mit dem $-Symbol ohne in\neine Falle zu tappen hast du das Spiel gewonnen.\nWenn du in eine Falle tappst hast du das Spiel\nleider verloren.");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Dein Schatzsuche-Abenteuer beginnt jetzt. Los geht's!");
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Zum Spielen ENTER drücken!");
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("*** Copyright: 2024 Tobias Lauer ***");
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

            for (int i = 0; i < 20; i++)
            {
                int falleX, falleY;

                do
                {
                    falleX = zufall.Next(0, 10);
                    falleY = zufall.Next(0, 10);

                } while ((falleX == spielerX && falleY == spielerY) || (falleX == schatzX && falleY == schatzY) || spielfeld[falleY, falleX].HatFalle);

                spielfeld[falleY, falleX].HatFalle = true;
                spielfeld[falleY, falleX].AktualisiereFeld();
            }            
        
            Console.WriteLine();
            Console.ResetColor();
            Console.ReadKey();

            ZeichneSpielfeld(spielfeld);

            Spielen(spielfeld, ref spielerX, ref spielerY);
        }

        static void ZeichneSpielfeld(Einzelfeld[,] spielfeld)
        {
            Console.BackgroundColor = ConsoleColor.DarkBlue;
            Console.Clear();

            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\r\n   _    _____     _                              __   _____          _                       _  \r\n  | |  | ____|___| |__   ___   ___  ___    ___  / _| |  ___|__  _ __| |_ _   _ _ __   ___   | | \r\n / __) |  _| / __| '_ \\ / _ \\ / _ \\/ __|  / _ \\| |_  | |_ / _ \\| '__| __| | | | '_ \\ / _ \\ / __)\r\n \\__ \\ | |__| (__| | | | (_) |  __/\\__ \\ | (_) |  _| |  _| (_) | |  | |_| |_| | | | |  __/ \\__ \\\r\n (   / |_____\\___|_| |_|\\___/ \\___||___/  \\___/|_|   |_|  \\___/|_|   \\__|\\__,_|_| |_|\\___| (   /\r\n  |_|                                                                                       |_| \r\n");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("*** ECHOES OF FORTUNE - Dein spannendes 2D-Konsolen-Schatzsuche-Abenteuer. ***");
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("************************************************************************************************");            
            Console.WriteLine("Finde den wertvollen Schatz. Viel Glück bei deiner Suche.");
            Console.WriteLine();
            
            Console.ForegroundColor = ConsoleColor.White;

            for (int y = 0; y < 10; y++)
            {
                for (int x = 0; x < 10; x++)
                {
                    // Farbe basierend auf den Feldinhalten setzen
                    if (spielfeld[y, x].HatSpieler)
                        Console.ForegroundColor = ConsoleColor.Red; // Spieler in Blau
                    else if (spielfeld[y, x].HatSchatz)
                        Console.ForegroundColor = ConsoleColor.Yellow; // Schatz in Gelb
                    else if (spielfeld[y, x].HatFalle)
                        Console.ForegroundColor = ConsoleColor.Green; // Fallen in Rot
                    else
                        Console.ForegroundColor = ConsoleColor.White;
                    Console.Write(spielfeld[y, x].Symbol + " ");
                }

                Console.WriteLine();
            }

            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Bewege deinen Spieler mit den folgenden Tasten:");
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("W = nach oben, A = nach links, S = nach unten, D = nach rechts.");
            Console.WriteLine();
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("*** Copyright: 2024 Tobias Lauer ***");
            Console.ResetColor();
        }

        static void Spielen(Einzelfeld[,] spielfeld, ref int spielerX, ref int spielerY)
        {
            while (true)
            {
                ConsoleKey eingabe = Console.ReadKey(true).Key;

                int neuerX = spielerX;
                int neuerY = spielerY;

                switch (eingabe)
                {
                    case ConsoleKey.W:
                        neuerY = Math.Max(0, spielerY - 1);
                        break;

                    case ConsoleKey.S:
                        neuerY = Math.Min(9, spielerY + 1);
                        break;

                    case ConsoleKey.A:
                        neuerX = Math.Max(0, spielerX - 1);
                        break;

                    case ConsoleKey.D:
                        neuerX = Math.Min(9, spielerX + 1);
                        break;

                    default:
                        Console.WriteLine("Ungültige Taste. Bitte w, a, s, d benutzen.");
                        continue;
                }

                Console.Clear();

                if (spielfeld[neuerY, neuerX].HatFalle)
                {
                    Console.BackgroundColor = ConsoleColor.DarkBlue;
                    Console.Clear();

                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("\r\n   _    _____     _                              __   _____          _                       _  \r\n  | |  | ____|___| |__   ___   ___  ___    ___  / _| |  ___|__  _ __| |_ _   _ _ __   ___   | | \r\n / __) |  _| / __| '_ \\ / _ \\ / _ \\/ __|  / _ \\| |_  | |_ / _ \\| '__| __| | | | '_ \\ / _ \\ / __)\r\n \\__ \\ | |__| (__| | | | (_) |  __/\\__ \\ | (_) |  _| |  _| (_) | |  | |_| |_| | | | |  __/ \\__ \\\r\n (   / |_____\\___|_| |_|\\___/ \\___||___/  \\___/|_|   |_|  \\___/|_|   \\__|\\__,_|_| |_|\\___| (   /\r\n  |_|                                                                                       |_| \r\n");
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("*** ECHOES OF FORTUNE - Dein spannendes 2D-Konsolen-Schatzsuche-Abenteuer. ***");
                    Console.WriteLine();
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("******************************************************************************");
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\r\n  _ _ _                  _                        _ _ _ \r\n | | | | __   _____ _ __| | ___  _ __ ___ _ __   | | | |\r\n | | | | \\ \\ / / _ \\ '__| |/ _ \\| '__/ _ \\ '_ \\  | | | |\r\n |_|_|_|  \\ V /  __/ |  | | (_) | | |  __/ | | | |_|_|_|\r\n (_|_|_)   \\_/ \\___|_|  |_|\\___/|_|  \\___|_| |_| (_|_|_)\r\n                                                        \r\n");
                    Console.ForegroundColor = ConsoleColor.Yellow;                   
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Hoppla bist in eine Falle getappt und hast das Spiel leider verloren.");
                    Console.WriteLine();
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine("Möchtest du es erneut versuchen?");
                    Console.WriteLine();
                    Console.WriteLine("Drücke jetzt J = neues Spiel oder N = Spiel beenden.");
                    Console.WriteLine();
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("******************************************************************************");
                    Console.WriteLine();
                    Console.WriteLine();
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine("*** Copyright: 2024 Tobias Lauer ***");
                    Console.ReadKey();
                    Console.ResetColor();
                }

                if (spielfeld[neuerY, neuerX].HatSchatz)
                {
                    Console.BackgroundColor = ConsoleColor.DarkBlue;
                    Console.Clear();

                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("\r\n   _    _____     _                              __   _____          _                       _  \r\n  | |  | ____|___| |__   ___   ___  ___    ___  / _| |  ___|__  _ __| |_ _   _ _ __   ___   | | \r\n / __) |  _| / __| '_ \\ / _ \\ / _ \\/ __|  / _ \\| |_  | |_ / _ \\| '__| __| | | | '_ \\ / _ \\ / __)\r\n \\__ \\ | |__| (__| | | | (_) |  __/\\__ \\ | (_) |  _| |  _| (_) | |  | |_| |_| | | | |  __/ \\__ \\\r\n (   / |_____\\___|_| |_|\\___/ \\___||___/  \\___/|_|   |_|  \\___/|_|   \\__|\\__,_|_| |_|\\___| (   /\r\n  |_|                                                                                       |_| \r\n");
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("*** ECHOES OF FORTUNE - Dein spannendes 2D-Konsolen-Schatzsuche-Abenteuer. ***");
                    Console.WriteLine();
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("******************************************************************************");                    
                    Console.WriteLine("\r\n   _   _   _                                                       _   _   _  \r\n  | | | | | |    __ _  _____      _____  _ __  _ __   ___ _ __    | | | | | | \r\n / __) __) __)  / _` |/ _ \\ \\ /\\ / / _ \\| '_ \\| '_ \\ / _ \\ '_ \\  / __) __) __)\r\n \\__ \\__ \\__ \\ | (_| |  __/\\ V  V / (_) | | | | | | |  __/ | | | \\__ \\__ \\__ \\\r\n (   (   (   /  \\__, |\\___| \\_/\\_/ \\___/|_| |_|_| |_|\\___|_| |_| (   (   (   /\r\n  |_| |_| |_|   |___/                                             |_| |_| |_| \r\n");
                    Console.ForegroundColor = ConsoleColor.Yellow;                  
                    Console.WriteLine("Gratulation, du hast den wertvollen Schatz gefunden und das Spiel gewonnen.");
                    Console.WriteLine();                    
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine("Möchtest du es erneut versuchen?");
                    Console.WriteLine();
                    Console.WriteLine("Drücke jetzt J = neues Spiel oder N = Spiel beenden.");
                    Console.WriteLine();
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("******************************************************************************");
                    Console.WriteLine();
                    Console.WriteLine();
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine("*** Copyright: 2024 Tobias Lauer ***");
                    Console.ReadKey();
                    Console.ResetColor();
                    break;
                }

                spielfeld[spielerY, spielerX].HatSpieler = false;
                spielfeld[spielerY, spielerX].AktualisiereFeld();

                spielerX = neuerX;
                spielerY = neuerY;
                spielfeld[spielerY, spielerX].HatSpieler = true;
                spielfeld[spielerY, spielerX].AktualisiereFeld();

                ZeichneSpielfeld(spielfeld);
            }
        }

        public static void SpieleMusik(string dateiPfad)        {
            
                try
                {
                using (var audioFile = new AudioFileReader(dateiPfad))
                using (var outputDevice = new WaveOutEvent())

                {
                    outputDevice.Init(audioFile);

                    while (true)
                    {
                        outputDevice.Volume = 0.5f;
                        outputDevice.Play();                       
                                        
                        if (outputDevice.PlaybackState == PlaybackState.Playing)
                        {
                            Thread.Sleep(100);
                            if(audioFile.Length == audioFile.Position) audioFile.Position = 0;
                        }                   
                                            
                    }

                }  
            
                }                  

                catch (Exception ex)
                {
                    Console.WriteLine($"Fehler beim abspielen der Musik: {ex.Message}");
                }         
        }
    }
}
