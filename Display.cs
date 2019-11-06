using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading;

namespace battleship
{
    public static class Display
    {
        //For underlining characters in the console
        const int STD_OUTPUT_HANDLE = -11;
        const uint ENABLE_VIRTUAL_TERMINAL_PROCESSING = 4;
        [DllImport("kernel32.dll", SetLastError = true)]
        static extern IntPtr GetStdHandle(int nStdHandle);
        [DllImport("kernel32.dll")]
        static extern bool GetConsoleMode(IntPtr hConsoleHandle, out uint lpMode);
        [DllImport("kernel32.dll")]
        static extern bool SetConsoleMode(IntPtr hConsoleHandle, uint dwMode);

        private static string LOGO_PATH = @"..\..\..\assets\images\logo.txt";
        private static string BATTLESHIP_PATH = @"..\..\..\assets\images\battleship.txt";
        private static string EXPLOSION_PATH = @"..\..\..\assets\images\explosion.txt";
        private static string MISS_PATH = @"..\..\..\assets\images\miss.txt";
        private static string[] PLAYER_SELECTION = { "  One Player  ", "  Two Players  " };
        private static int PLAYER_SELECTION_LINE_NUMBER = 11;
        private static string NUMBER_OF_PLAYERS_PROMPT = "How many players? Use the left and right arrow keys to choose and press enter to submit.";
        private static string PLACE_BOAT_PROMPT = "Use the arrow keys to move the boat, spacebar to change orientation, and enter to place.";
        private static string ATTACK_PROMPT = "Enter a coordinate and press enter to fire. Exampes: A6, J7, D1.";
        private static string PRESS_KEY_TO_CONTINUE_PROMPT = "press any key to continue.";

        public static void DisplayWelcome()
        {
            SetConsoleColors();
            ReadAndDisplayFile(LOGO_PATH);
            DisplayPrompt(NUMBER_OF_PLAYERS_PROMPT);
            DisplayCurrentlySelectedNumberOfPlayers(NumberOfHumanPlayers.OnePlayer);
            ReadAndDisplayFile(BATTLESHIP_PATH);
        }

        private static void DisplayPrompt(string prompt)
        {
            Console.WriteLine();
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
            int lengthOfLongestLine = 0;
            foreach (string line in fileContentsByLine)
                if (line.Length > lengthOfLongestLine) lengthOfLongestLine = line.Length;


            foreach (string line in fileContentsByLine)
                CenterText(line, lengthOfLongestLine);
        }

        public static void CenterText(String text, int widthToSubtract)
        {
            Console.Write(new string(' ', (Console.WindowWidth - widthToSubtract) / 2));
            Console.WriteLine(text);
        }

        public static void CenterTextWithoutReturn(String text, int widthToSubtract)
        {
            Console.Write(new string(' ', (Console.WindowWidth - widthToSubtract) / 2));
            Console.Write(text);
        }

        public static void DisplayBoatPlacing(Game game)
        {
            Display.DisplayBetweenTurns(game);
            ReadAndDisplayFile(LOGO_PATH);
            DisplayPrompt(PLACE_BOAT_PROMPT);
            game.ActivePlayer.Board.DisplayToOwner();
        }

        public static string WriteUnderline(string stringToUnderline)
        {
            var handle = GetStdHandle(STD_OUTPUT_HANDLE);
            uint mode;
            GetConsoleMode(handle, out mode);
            mode |= ENABLE_VIRTUAL_TERMINAL_PROCESSING;
            SetConsoleMode(handle, mode);
            return $"\x1B[4m{stringToUnderline}\x1B[24m";
        }

        public static void WriteAt(string s, int row, int column)
        {
            try
            {
                Console.SetCursorPosition(column, row);
                Console.Write(s);

                // moves cursor outside of GameBoard
                Console.SetCursorPosition(0, 15);
            }
            catch (ArgumentOutOfRangeException e)
            {
                Console.Clear();
                Console.WriteLine(e.Message);
            }
        }

        public static void DisplayOpponentBoard(Game game)
        {
            Display.DisplayBetweenTurns(game);
            ReadAndDisplayFile(LOGO_PATH);
            DisplayPrompt(ATTACK_PROMPT + "\n\n\n");
            game.ActivePlayer.Opponent.Board.DisplayToOpponent();
        }

        public static void DisplayBetweenTurns(Game game)
        {
            Console.Clear();
            ReadAndDisplayFile(LOGO_PATH);
            Console.WriteLine("\n\n\n");
            DisplayPrompt($"{game.ActivePlayer.Name}, {PRESS_KEY_TO_CONTINUE_PROMPT}");
            Console.ReadKey(true);
            Console.WriteLine();
            Console.Clear();
        }

        public static void DisplayHit()
        {
            Console.Clear();
            ReadAndDisplayFile(EXPLOSION_PATH);
            Thread.Sleep(1000);
        }

        public static void DisplayMiss()
        {
            Console.Clear();
            ReadAndDisplayFile(MISS_PATH);
            Thread.Sleep(1000);
        }
    }
}
