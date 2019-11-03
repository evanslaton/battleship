using System;
using System.IO;

namespace battleship
{
    public static class Display
    {
        private static string LOGO_PATH = @"..\..\..\assets\images\logo.txt";
        private static string BATTLESHIP_PATH = @"..\..\..\assets\images\battleship.txt";
        private static string[] PLAYER_SELECTION = { "  One Player  ", "  Two Players  " };
        private static int PLAYER_SELECTION_LINE_NUMBER = 11;
        private static int WIDTH_OF_LONGEST_LINE = 47;

        public static void DisplayWelcome()
        {
            SetConsoleColors();
            ReadAndDisplayFile(LOGO_PATH);
            DisplayPlayerSelectionPrompt();
            DisplayCurrentlySelectedNumberOfPlayers(NumberOfHumanPlayers.OnePlayer);
            ReadAndDisplayFile(BATTLESHIP_PATH);
        }

        private static void DisplayPlayerSelectionPrompt()
        {
            Console.WriteLine();
            string prompt = "How many players? Use the left and right arrow keys to choose and press enter to submit.";
            CenterText(prompt, prompt.Length);
            Console.WriteLine();
        }

        public static void DisplayCurrentlySelectedNumberOfPlayers(NumberOfHumanPlayers numberOfPlayers)
        {
            int SPACE_BETWEEN_CHOICES = 5;
            int offset = Console.WindowWidth - PLAYER_SELECTION[0].Length - PLAYER_SELECTION[1].Length - SPACE_BETWEEN_CHOICES;
            Console.SetCursorPosition(0, PLAYER_SELECTION_LINE_NUMBER);
            Console.Write(new string(' ', offset / 2));
            if (numberOfPlayers == NumberOfHumanPlayers.OnePlayer)
                HighlightOnePlayerOption();
            else
                HighlightTwoPlayerOption();
            Console.WriteLine();
        }

        private static void HighlightOnePlayerOption()
        {
            SetHighlightColors();
            Console.Write(PLAYER_SELECTION[0]);
            ResetColors();
            Console.WriteLine($"     {PLAYER_SELECTION[1]}");
        }

        private static void HighlightTwoPlayerOption()
        {
            Console.Write(PLAYER_SELECTION[0]);
            Console.Write("     ");
            SetHighlightColors();
            Console.WriteLine(PLAYER_SELECTION[1]);
            ResetColors();
        }

        private static void SetConsoleColors()
        {
            Console.ForegroundColor = ConsoleColor.Black;
            Console.BackgroundColor = ConsoleColor.Cyan;
            Console.Clear();
        }

        private static void SetHighlightColors()
        {
            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Blue;
        }

        private static void ResetColors()
        {
            Console.ForegroundColor = ConsoleColor.Black;
            Console.BackgroundColor = ConsoleColor.Cyan;
        }

        private static void ReadAndDisplayFile(string filePath)
        {
            string[] fileContentsByLine = File.ReadAllLines(filePath);
            foreach (string line in fileContentsByLine)
                CenterText(line, WIDTH_OF_LONGEST_LINE);
        }

        private static void CenterText(String text, int widthToSubtract)
        {
            Console.Write(new string(' ', (Console.WindowWidth - widthToSubtract) / 2));
            Console.WriteLine(text);
        }
    }
}
