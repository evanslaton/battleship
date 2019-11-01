using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace battleship
{
    public class Game
    {
        private static string LOGO_PATH = @"..\..\..\assets\images\logo.txt";
        private static string BATTLESHIP_PATH = @"..\..\..\assets\images\battleship.txt";
        private static string[] PLAYER_SELECTION = { "  One Player  ", "  Two Players  " };
        private static int PLAYER_SELECTION_LINE_NUMBER = 11;
        private static int WIDTH_OF_LONGEST_LINE = 47;

        Player ActivePlayer { get; set; }
        Player InactivePlayer { get; set; }
        SimplePlayerFactory SimplePlayerFactory { get; set; }

        public static void Start()
        {
            Game g = new Game();
            g.DisplayWelcome();
            Console.ReadKey();
        }


        private void DisplayWelcome()
        { 
            SetConsoleColors();
            ReadAndDisplayFile(LOGO_PATH);
            PrintPlayerSelectionPrompt();
            ReadAndDisplayFile(BATTLESHIP_PATH);
            Console.SetCursorPosition(0, PLAYER_SELECTION_LINE_NUMBER);
        }

        private static void PrintPlayerSelectionPrompt()
        {
            Console.WriteLine();
            string prompt = "How many players? Use the left and right arrow keys to choose and press enter to submit";
            CenterText(prompt, prompt.Length);
            int DEFAULT_NUMBER_OF_PLAYERS = 1;
            DisplayNumberOfPlayers(DEFAULT_NUMBER_OF_PLAYERS);
        }

        private static void DisplayNumberOfPlayers(int numberOfPlayers)
        {
            Console.WriteLine();
            int SPACE_BETWEEN_CHOICES = 5;
            int offset = Console.WindowWidth - PLAYER_SELECTION[0].Length - PLAYER_SELECTION[1].Length - SPACE_BETWEEN_CHOICES;
            Console.Write(new string(' ', offset / 2));

            if (numberOfPlayers == 1) SelectOnePlayer();
            else SelectTwoPlayers();
        }

        private static void SelectOnePlayer()
        {
            SetPlayerSelectionColors();
            Console.Write(PLAYER_SELECTION[0]);
            ResetColors();
            Console.WriteLine($"     {PLAYER_SELECTION[0]}");
        }

        private static void SelectTwoPlayers()
        {
            Console.Write(PLAYER_SELECTION[0]);
            SetPlayerSelectionColors();
            Console.WriteLine($"     {PLAYER_SELECTION[1]}");
            ResetColors();
        }

        private static void SetConsoleColors()
        {
            Console.ForegroundColor = ConsoleColor.Black;
            Console.BackgroundColor = ConsoleColor.Cyan;
            Console.Clear();
        }

        private static void SetPlayerSelectionColors()
        {
            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Blue;
        }

        private static void ResetColors()
        {
            Console.ForegroundColor = ConsoleColor.Black;
            Console.BackgroundColor = ConsoleColor.Cyan;
        }

        private void ReadAndDisplayFile(string filePath)
        {
            string[] fileContentsByLine = File.ReadAllLines(filePath);
            foreach (string line in fileContentsByLine)
            {
                CenterText(line, WIDTH_OF_LONGEST_LINE);
            }
        }

        private static void CenterText(String text, int widthToSubtract)
        {
            Console.Write(new string(' ', (Console.WindowWidth - widthToSubtract) / 2));
            Console.WriteLine(text);
        }
    }
}
