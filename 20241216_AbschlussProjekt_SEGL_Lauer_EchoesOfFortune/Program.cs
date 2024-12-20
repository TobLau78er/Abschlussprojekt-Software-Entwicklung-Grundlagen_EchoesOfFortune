using System.ComponentModel.Design;
using System.Linq.Expressions;
using System.Threading;
using NAudio.Wave; //Bibliotheken des Projekts z.B. NAudio für Implementierung Musik.wav.

namespace _20241216_AbschlussProjekt_SEGL_Lauer_EchoesOfFortune //Projektbeginn: Hier fängt das Spiel an.
{
    internal class Program //Die Projekt-Hauptklasse und Start des Programm.
    {
        static void Main(string[] args) //Die Projekt-Hauptmethode "Main" innerhalb welcher unser Spiel läuft.
        {
            Console.BackgroundColor = ConsoleColor.DarkBlue; //Das Spiel hat die Konsolenfenster Hintergrundfarbe "DunkelBlau" erhalten.
            Console.Clear(); //Hiermit wird die Hintergrundfarbe übernommen.

            Console.ForegroundColor = ConsoleColor.Yellow; //Das ASCII-Spielelogo oben im Konsolenfenster hat die Farbe "Gelb".
            Console.WriteLine("\r\n   _    _____     _                              __   _____          _                       _  \r\n  | |  | ____|___| |__   ___   ___  ___    ___  / _| |  ___|__  _ __| |_ _   _ _ __   ___   | | \r\n / __) |  _| / __| '_ \\ / _ \\ / _ \\/ __|  / _ \\| |_  | |_ / _ \\| '__| __| | | | '_ \\ / _ \\ / __)\r\n \\__ \\ | |__| (__| | | | (_) |  __/\\__ \\ | (_) |  _| |  _| (_) | |  | |_| |_| | | | |  __/ \\__ \\\r\n (   / |_____\\___|_| |_|\\___/ \\___||___/  \\___/|_|   |_|  \\___/|_|   \\__|\\__,_|_| |_|\\___| (   /\r\n  |_|                                                                                       |_| \r\n");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("*** Willkommen beim Spiel ECHOES OF FORTUNE ***"); //Der Bergrüßungstext hat die Farbe "Grün".
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Yellow; 
            Console.WriteLine("************************************************************************************************"); //Die Zeile mit den gelben Sternen unterteilt die Konsole in drei Bereiche. Ganz oben der Bereich Spiellogo und Zusatzinfos. In der Mitte innerhalb den beiden Zeilen mit den gelben Sternen ist der sogenannte Anwendungs-Hauptbereich in welchem z.B. der Spielstart und die Spielanleitung, das Spiel selbst und die Sieg- und Gewinn-Sequenzen ablaufen/stattfinden. Diese Konfiguration erstreckt sich einheitlich über alle entsprechenden Bereiche/Sequenzen der Anwendung/des Spiels.
            Console.WriteLine();

            string musikPfad = "C:\\Users\\LauerTobias\\source\\repos\\20241216_AbschlussProjekt_SEGL_Lauer_EchoesOfFortune\\musik.wav"; //Die Datei Musik.wav unter diesem Pfad wird im Hintergrund während des Spiels abgespielt.

            Thread musikThread = new Thread(() => SpieleMusik(musikPfad)); //Hierzu wird ein Hintergrund-Thread eingesetzt. Durch Multithreading ist es damit möglich während das Spiel läuft im Hintergrund Musik abzuspielen. Das Spiel und die Musik laufen somit jeweils in unterschiedlichen Threads ab.
            musikThread.IsBackground = true; //Der Hintergrund-Thread stoppt automatisch wenn die Anwendung beendet wird.
            musikThread.Start(); //Über den Einsatz dieser Methode startet das abspielen der Musik.

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Dein spannendes 2D-Konsolen-Schatzsuche-Abenteuer."); //Fast den Grundgedanken des Spiels zusammen. Soll den User dabei ansprechen damit er richtig Lust bekommt jetzt direkt das Spiel zu spielen.
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Anleitung:"); //Hier eine kurze, einfache und klar verständlich sowie gleichzeitig für den User ansprechend aufgebaute Spielanleitung.
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("Als Spieler hast du das P-Symbol auf dem Spielfeld.\nDu musst den Schatz mit dem $-Symbol finden ohne\ndabei in eine Falle zu tappen. Bewege dein\nP-Symbol mit den folgenden Tasten: w = nach oben,\na = nach links, s = nach unten, d = nach rechts.\nErreichst du den Schatz mit dem $-Symbol ohne in\neine Falle zu tappen hast du das Spiel gewonnen.\nWenn du in eine Falle tappst hast du das Spiel\nleider verloren.");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine();            
            Console.WriteLine("Dein Schatzsuche-Abenteuer beginnt jetzt. Los geht's!");
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Zum Spielen ENTER drücken!"); //Mit "Enter" kann der User nun das Spiel starten.
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("************************************************************************************************"); //Unter der zweiten gelben Zeile mit Sternen beginnt der Abschluss-Bereich der Anwendung.     
            Console.WriteLine();
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("*** Copyright: 2024 Tobias Lauer / Music by: Tobias Lauer ***"); //Hier ist das Copyright sowie die Musikherkunft/-Clearing implementiert.    
            Console.ResetColor();

            Einzelfeld[,] spielfeld = new Einzelfeld[10, 10]; //Das Spielfeld (10x10 Raster) wird über ein Mehrdimensionales-Array (2D-Array) aus jeweils einzelnen Objekten namens "Einzelfeld" aus der Klasse "Einzelfeld" initialisiert.

            for (int y = 0; y < 10; y++) //Mit den beiden folgenden For-Schleife wird das Spielfeld (10x10 Raster) mit Zeile "x" und Spalte "y" erstellt und die jeweiligen Anfangswerte der einzelnen Felder gesetzt.
            {
                for (int x = 0; x < 10; x++)//Weitere der beiden For-Schleifen für die Erstellung des Spielfeld.
                {
                    spielfeld[y, x] = new Einzelfeld //jedes Feld des Spielfeld wird als Objekt "Einzelfeld" ersellt.
                    {
                        PosZeile = x, //Gibt die Zeilenposition des Feldes an.
                        PosSpalte = y, //Gibt die Spaltenposition des Feldes an.
                        HatSpieler = false, //Es wird hiermit definiert, dass erstmal kein Spieler auf dem Feld ist.
                        HatSchatz = false, //Es wird hiermit definiert, dass erstmal kein Schatz auf dem Feld ist.
                        HatFalle = false, //Es wird hiermit definiert, dass erstmal kein Falle auf dem Feld ist.
                    };

                    spielfeld[y, x].AktualisiereFeld(); //Die Methode aktualisiert die Anzeige des Spielfelds.
                }

            }

            Random zufall = new Random(); //Hier wird per Random ein Zufallsgenerator initialisiert um die Startposition des Spieler zufällig zu bestimmen.

            int spielerX = zufall.Next(0, 10); //Hier wird über die Methode "Next" aus der Klasse "Random" der Spieler zufällig auf das Spielfeld gesetzt.
            int spielerY = zufall.Next(0, 10);

            spielfeld[spielerY, spielerX].HatSpieler = true;
            spielfeld[spielerY, spielerX].AktualisiereFeld();

            int schatzX, schatzY;

            do //Hier wird mit einer Do-While-Schleife der Schatz zufällig auf das Spielfeld gesetzt und er darf dabei nicht die gleiche Position wie der Spieler haben.
            {
                schatzX = zufall.Next(0, 10);
                schatzY = zufall.Next(0, 10);

            } while (schatzX == spielerX && schatzY == spielerY);

            spielfeld[schatzY, schatzX].HatSchatz = true;
            spielfeld[schatzY, schatzX].AktualisiereFeld();

            for (int i = 0; i < 20; i++) //Hier werden mit einer For-Schleife und einer darin verschachtelten Do-While-Schleife die 20 Fallen auf dem Spielfeld platziert. Dabei wird geprüft damit keine Falle auf dem Spielerfeld, Schatzfeld oder bereits mit einer Falle belegten Feld platziert wird.
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

            ZeichneSpielfeld(spielfeld); //Hier wird die Methode "ZeichneSpielfeld" aufgerufen damit das Spielfeld gezeichnet wird und der User das Startlayout/Spielfeld sehen kann.

            Spielen(spielfeld, ref spielerX, ref spielerY); //Hier wird die Methode "Spielen" aufgerufen über welche die Spieleraktion und im Grunde das eigentliche Spiel ausgeführt wird.
        }

        static void ZeichneSpielfeld(Einzelfeld[,] spielfeld) //Hier wird die Methode "ZeichneSpielfeld" erstellt. Sie dient dazu das Spielfeld zu zeichnen.
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
            Console.WriteLine();
            Console.WriteLine("Die Schatzsuche beginnt jetzt!");
            Console.WriteLine();
            
            Console.ForegroundColor = ConsoleColor.White;

            for (int y = 0; y < 10; y++) //Diese For-Schleife erstellt die Spalten des Spielfelds.
            {
                for (int x = 0; x < 10; x++) //Diese For-Schleife erstellt die Zeilen des Spielfelds.
                {
                    //Hier wird die Farbe der Symbole für Spieler, Schatz und Fallen basierend auf den Feldinhalten gesetzt.
                    if (spielfeld[y, x].HatSpieler)
                        Console.ForegroundColor = ConsoleColor.Red; //Spieler in Rot.
                    else if (spielfeld[y, x].HatSchatz)
                        Console.ForegroundColor = ConsoleColor.Yellow; //Schatz in Gelb.
                    else if (spielfeld[y, x].HatFalle)
                        Console.ForegroundColor = ConsoleColor.Green; //Fallen in Grün.
                    else
                        Console.ForegroundColor = ConsoleColor.White; //Leere Felder in Weiß.
                    Console.Write(spielfeld[y, x].Symbol + " "); //Für jedes Feld das Symbold ausgeben.
                }

                Console.WriteLine(); //Hierüber wird ein Zeilenumbruch nach jeder Spielfeldreihe erzeugt.
            }

            Console.WriteLine();
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Bewege deinen Spieler mit den folgenden Tasten:");
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("w = nach oben, a = nach links, s = nach unten, d = nach rechts.");
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("************************************************************************************************");
            Console.WriteLine();
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("*** Copyright: 2024 Tobias Lauer / Music by: Tobias Lauer ***");
            Console.ResetColor();
        }

        static void Spielen(Einzelfeld[,] spielfeld, ref int spielerX, ref int spielerY) 
        {
            while (true) //Diese Hauptschleife läuft solange bis das Spiel gewonnen oder verloren ist.
            {
                ConsoleKey eingabe = Console.ReadKey(true).Key; //Hier wird die Taste welche der Spieler während der Spielersteuerung drückt eingelesen.

                int neuerX = spielerX; //Hier wird die neue Zeilenposition des Spielers gespeichert.
                int neuerY = spielerY; //Hier wird die neue Spaltenposition des Spielers gespeichert.

                switch (eingabe) //Der Switch-Case prüft welche Taste gedrückt wird.
                {
                    case ConsoleKey.W: //w = Bewegung nach oben.
                        neuerY = Math.Max(0, spielerY - 1); 
                        break;

                    case ConsoleKey.S: // s = Bewegung nach hinten.
                        neuerY = Math.Min(9, spielerY + 1);
                        break;

                    case ConsoleKey.A: // s = Bewegung nach links.
                        neuerX = Math.Max(0, spielerX - 1);
                        break;

                    case ConsoleKey.D: // s = Bewegung nach rechts.
                        neuerX = Math.Min(9, spielerX + 1);
                        break;

                        continue;
                }

                Console.Clear(); //Konsole wird gelöscht für nächsten Zustand.

                if (spielfeld[neuerY, neuerX].HatFalle) //Hier wird geprüft ob der Spieler auf ein Falle tritt.
                {
                    Console.BackgroundColor = ConsoleColor.DarkBlue;
                    Console.Clear();

                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("\r\n   _    _____     _                              __   _____          _                       _  \r\n  | |  | ____|___| |__   ___   ___  ___    ___  / _| |  ___|__  _ __| |_ _   _ _ __   ___   | | \r\n / __) |  _| / __| '_ \\ / _ \\ / _ \\/ __|  / _ \\| |_  | |_ / _ \\| '__| __| | | | '_ \\ / _ \\ / __)\r\n \\__ \\ | |__| (__| | | | (_) |  __/\\__ \\ | (_) |  _| |  _| (_) | |  | |_| |_| | | | |  __/ \\__ \\\r\n (   / |_____\\___|_| |_|\\___/ \\___||___/  \\___/|_|   |_|  \\___/|_|   \\__|\\__,_|_| |_|\\___| (   /\r\n  |_|                                                                                       |_| \r\n");
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("*** ECHOES OF FORTUNE - Dein spannendes 2D-Konsolen-Schatzsuche-Abenteuer. ***");
                    Console.WriteLine();
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("************************************************************************************************");
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine();
                    Console.WriteLine("Du bist in eine Falle getappt und hast das Spiel leider verloren.");
                    Console.WriteLine();
                    Console.WriteLine("\r\n  _ _ _                  _                        _ _ _ \r\n | | | | __   _____ _ __| | ___  _ __ ___ _ __   | | | |\r\n | | | | \\ \\ / / _ \\ '__| |/ _ \\| '__/ _ \\ '_ \\  | | | |\r\n |_|_|_|  \\ V /  __/ |  | | (_) | | |  __/ | | | |_|_|_|\r\n (_|_|_)   \\_/ \\___|_|  |_|\\___/|_|  \\___|_| |_| (_|_|_)\r\n                                                        \r\n");                    
                    Console.WriteLine();                 
                    Console.WriteLine();
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine("Möchtest du es erneut versuchen?");
                    Console.WriteLine();
                    Console.Write("Drücke jetzt j = neues Spiel oder n = Spiel beenden und bestätige mit ENTER: ");                    
                    Console.WriteLine();
                    Console.WriteLine();
                    Console.WriteLine();
                    Console.WriteLine();                    
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("************************************************************************************************");
                    Console.WriteLine();
                    Console.WriteLine();
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine("*** Copyright: 2024 Tobias Lauer / Music by: Tobias Lauer ***");
                    Console.ResetColor();

                    string input = Console.ReadLine().ToLower(); //Hier wird die Spielereingabe ob er j = neues Spiel oder n = spiel beenden auswählt bzw. über Falscheingabe informiert wird.

                    while (input != "j" && input != "n") //Die Abfrage erfolgt solange mit einer While-Schleife bis der Spieler entweder j = neues Spiel oder n = Spiel beenden auswählt.
                    {
                        Console.WriteLine();
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write("Falsche Eingabe. Bitte erneut eingeben: ");
                        input = Console.ReadLine().ToLower();
                        Console.ResetColor();
                    }                                       
                    if (input == "n") //Hier wird das Spiel beendet wenn der Spieler n = Spiel beenden auswählt.
                    {
                        Environment.Exit(0);
                    }                      
                }
                if (spielfeld[neuerY, neuerX].HatSchatz) //Hier wird geprüft ob der Spieler auf den Schatz tritt.
                {
                    Console.BackgroundColor = ConsoleColor.DarkBlue;
                    Console.Clear();

                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("\r\n   _    _____     _                              __   _____          _                       _  \r\n  | |  | ____|___| |__   ___   ___  ___    ___  / _| |  ___|__  _ __| |_ _   _ _ __   ___   | | \r\n / __) |  _| / __| '_ \\ / _ \\ / _ \\/ __|  / _ \\| |_  | |_ / _ \\| '__| __| | | | '_ \\ / _ \\ / __)\r\n \\__ \\ | |__| (__| | | | (_) |  __/\\__ \\ | (_) |  _| |  _| (_) | |  | |_| |_| | | | |  __/ \\__ \\\r\n (   / |_____\\___|_| |_|\\___/ \\___||___/  \\___/|_|   |_|  \\___/|_|   \\__|\\__,_|_| |_|\\___| (   /\r\n  |_|                                                                                       |_| \r\n");
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("*** ECHOES OF FORTUNE - Dein spannendes 2D-Konsolen-Schatzsuche-Abenteuer. ***");
                    Console.WriteLine();
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("************************************************************************************************");
                    Console.WriteLine();
                    Console.WriteLine("Du hast den wertvollen Schatz gefunden und das Spiel gewonnen.");
                    Console.WriteLine();
                    Console.WriteLine("\r\n   _   _   _                                                       _   _   _  \r\n  | | | | | |    __ _  _____      _____  _ __  _ __   ___ _ __    | | | | | | \r\n / __) __) __)  / _` |/ _ \\ \\ /\\ / / _ \\| '_ \\| '_ \\ / _ \\ '_ \\  / __) __) __)\r\n \\__ \\__ \\__ \\ | (_| |  __/\\ V  V / (_) | | | | | | |  __/ | | | \\__ \\__ \\__ \\\r\n (   (   (   /  \\__, |\\___| \\_/\\_/ \\___/|_| |_|_| |_|\\___|_| |_| (   (   (   /\r\n  |_| |_| |_|   |___/                                             |_| |_| |_| \r\n");                    
                    Console.WriteLine();                                     
                    Console.WriteLine();
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine("Möchtest du es erneut versuchen?");
                    Console.WriteLine();
                    Console.Write("Drücke jetzt j = neues Spiel oder n = Spiel beenden und bestätige mit ENTER: ");
                    Console.WriteLine();
                    Console.WriteLine();
                    Console.WriteLine();
                    Console.WriteLine();                    
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("************************************************************************************************");
                    Console.WriteLine();
                    Console.WriteLine();
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine("*** Copyright: 2024 Tobias Lauer / Music by: Tobias Lauer ***");
                    Console.ResetColor();

                    string input = Console.ReadLine().ToLower();

                    while (input != "j" && input != "n") //Der Spieler kann erneut j = neues Spiel oder n = Spiel beenden auswählen.
                    {
                        Console.WriteLine();
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write("Falsche Eingabe. Bitte erneut eingeben: ");
                        input = Console.ReadLine().ToLower();
                        Console.ResetColor();
                    }
                    if (input == "n") //Wählt der Spieler n = Spiel beenden wird das Spiel hier abgebrochen.
                    {
                        Environment.Exit(0);
                    }
                }

                spielfeld[spielerY, spielerX].HatSpieler = false; //Hier wird der Spieler vom vorherigen Feld entfernt.
                spielfeld[spielerY, spielerX].AktualisiereFeld(); //Hier wird das vorherige Feld aktualisiert.

                spielerX = neuerX; //Hier wird die neue Spieler Zeilenposition gesetzt.
                spielerY = neuerY; //Hier wird die neue Spieler Spaltenposition gesetzt.

                spielfeld[spielerY, spielerX].HatSpieler = true; //Hier wird der Spieler auf das neue Feld gesetzt.
                spielfeld[spielerY, spielerX].AktualisiereFeld(); //Hier wird das neue Feld aktualisiert.

                ZeichneSpielfeld(spielfeld); //Diese Methode zeichnet das Spielfeld in der Konsole.
            }
        }

        public static void SpieleMusik(string dateiPfad) //Diese Methode spielt Musik im Hintegrund ab.        
        {            
                try
                {
                using (var audioFile = new AudioFileReader(dateiPfad)) //Hier wird die Audiodatei geladen.
                using (var outputDevice = new WaveOutEvent()) //Hier wird ein Wiedergabegerät erstellt.

                {
                    outputDevice.Init(audioFile); //Hier wird die Audiodatei mit dem Wiedergabegerät verknüpft.

                    while (true)
                    {
                        outputDevice.Volume = 0.4f; //Hier wird die Lautstärke auf 40% gesetzt.
                        outputDevice.Play(); //Hier wird die Wiedergabe gestartet.                     
                                        
                        if (outputDevice.PlaybackState == PlaybackState.Playing)
                        {
                            Thread.Sleep(100); //Hier wird länge des Loops festgelegt.
                            if(audioFile.Length == audioFile.Position) audioFile.Position = 0;
                        }                               
                    }
                }              
                }                
                    catch (Exception ex)
                {
                    Console.WriteLine($"Fehler beim abspielen der Musik: {ex.Message}"); //Hier zeigt es eine Fehlermeldung an wenn beim abspielen etwas nicht funktioniert.
                }         
        }
    }
}
