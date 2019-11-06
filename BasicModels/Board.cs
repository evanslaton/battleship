using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace battleship
{
    public class Board
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

        private static readonly int BOARD_DIMENSION = 11;
        private static readonly string[] ROW_HEADERS = { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J" };
        private static readonly string[] COLUMN_HEADERS = { "1", "2", "3", "4", "5", "6", "7", "8", "9", "10" };
        public static int ROW_OFFSET = 12;
        public static int COLUMN_OFFSET = 50;
        private static string EMPTY_SPACE = $"|{Display.WriteUnderline(" ")}";
        private static string EMPTY_SPACE_RIGHT_EDGE = $"|{Display.WriteUnderline(" ")}|";
        private static string BOAT_SPACE = $"|{Display.WriteUnderline("O")}";
        private static string BOAT_SPACE_RIGHT_EDGE = $"|{Display.WriteUnderline("O")}|";
        private static string HIT_BOAT_SPACE = $"|{Display.WriteUnderline("X")}";
        private static string HIT_BOAT_SPACE_RIGHT_EDGE = $"|{Display.WriteUnderline("X")}|";
        private static string MISS_SPACE = $"|{Display.WriteUnderline("~")}";
        private static string MISS_SPACE_RIGHT_EDGE = $"|{Display.WriteUnderline("~")}|";

        public Boat[] Boats { get; set; }
        public string[,] GameBoard { get; set; }
        public int Lives { get; set; }

        public Board()
        {
            GameBoard = new string[BOARD_DIMENSION, BOARD_DIMENSION];
            FillBoard();
            Lives = 5;
        }

        private void FillBoard()
        {
            GameBoard[0, 0] = "   "; //Top corner of board is empty
            SetRowHeaders();
            SetColumnHeaders();
            SetCells();
        }

        public void DisplayToOwner()
        {
            StringBuilder stringBuilder = new StringBuilder();
            string currentRow;
            int OFFSET = 4;
            for (int row = 0; row < BOARD_DIMENSION; row++)
            {
                for (int column = 0; column < BOARD_DIMENSION; column++)
                {
                    stringBuilder.Append(GameBoard[row, column]);
                }
                currentRow = stringBuilder.ToString();
                Display.CenterText(currentRow, currentRow.Length / OFFSET);
                stringBuilder.Clear();
            }
        }

        public void DisplayToOpponent()
        {
            StringBuilder stringBuilder = new StringBuilder();
            string currentRow;
            int OFFSET = 4;
            for (int row = 0; row < BOARD_DIMENSION; row++)
            {
                for (int column = 0; column < BOARD_DIMENSION; column++)
                {
                    if (GameBoard[row, column] == BOAT_SPACE)
                        stringBuilder.Append(EMPTY_SPACE);
                    else if (GameBoard[row, column] == BOAT_SPACE_RIGHT_EDGE)
                        stringBuilder.Append(EMPTY_SPACE_RIGHT_EDGE);
                    else
                        stringBuilder.Append(GameBoard[row, column]);
                }
                currentRow = stringBuilder.ToString();
                Display.CenterText(currentRow, currentRow.Length / OFFSET);
                stringBuilder.Clear();
            }
        }

        private void SetRowHeaders()
        {
            for (int row = 0; row < BOARD_DIMENSION - 1; row++)
            {
                GameBoard[row + 1, 0] = $" {ROW_HEADERS[row]} ";
            }
        }

        private void SetColumnHeaders()
        {
            for (int column = 0; column < BOARD_DIMENSION - 1; column++)
            {
                GameBoard[0, column + 1] = $" {Display.WriteUnderline(COLUMN_HEADERS[column])}";
            }
        }

        private void SetCells()
        {
            for (int row = 1; row < BOARD_DIMENSION; row++)
            {
                for (int column = 1; column < BOARD_DIMENSION; column++)
                {
                    if (column == BOARD_DIMENSION - 1) GameBoard[row, column] = EMPTY_SPACE_RIGHT_EDGE;
                    else GameBoard[row, column] = EMPTY_SPACE;
                }
            }
        }

        public bool AddBoatToBoard(Boat boat, int row, int column)
        {
            if (boat.Orientation == Orientation.Horizontal && ValidHorizontalBoatLocation(boat, row, column))
            {
                AddHorizontalBoat(boat, row, 0, column, 0);
                return true;
            }
            else if (boat.Orientation == Orientation.Vertical && ValidVerticalBoatLocation(boat, row, column))
            {
                AddVerticalBoat(boat, row, 0, column, 0);
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool ValidHorizontalBoatLocation(Boat boat, int row, int column)
        {
            int boardRow = row - ROW_OFFSET + 1;
            int boardColumn = TranslateConsoleGridToGameBoard(column);
            for (int i = 0; i < boat.BoatLives.Length; i++)
            {
                if (GameBoard[boardRow, boardColumn + i] == BOAT_SPACE ||
                    GameBoard[boardRow, boardColumn + i] == BOAT_SPACE_RIGHT_EDGE) 
                { 
                    return false;
                }
            }
            return true;
        }

        private bool ValidVerticalBoatLocation(Boat boat, int row, int column)
        {
            int boardRow = row - ROW_OFFSET + 1;
            int boardColumn = TranslateConsoleGridToGameBoard(column);
            for (int i = 0; i < boat.BoatLives.Length; i++)
            {
                if (GameBoard[boardRow + i, boardColumn] == BOAT_SPACE ||
                    GameBoard[boardRow + i, boardColumn] == BOAT_SPACE_RIGHT_EDGE)
                {
                    return false;
                }
            }
            return true;
        }

        public void AddHorizontalBoat(Boat boat, int row, int rowOffset, int column, int columnOffset)
        { 
            int boardRow = row - ROW_OFFSET + 1;
            int boardColumn = TranslateConsoleGridToGameBoard(column);
            for (int i = 0; i < boat.BoatLives.Length; i++)
            { 
                if (GameBoard[boardRow, boardColumn + i] == EMPTY_SPACE)
                    GameBoard[boardRow, boardColumn + i] = BOAT_SPACE;
                else
                    GameBoard[boardRow, boardColumn + i] = BOAT_SPACE_RIGHT_EDGE;
            }
        }

        public void AddVerticalBoat(Boat boat, int row, int rowOffset, int column, int columnOffset)
        {
            int boardRow = row - ROW_OFFSET + 1;
            int boardColumn = TranslateConsoleGridToGameBoard(column);
            for (int i = 0; i < boat.BoatLives.Length; i++)
            {
                if (GameBoard[boardRow + i, boardColumn] == EMPTY_SPACE)
                    GameBoard[boardRow + i, boardColumn] = BOAT_SPACE;
                else
                    GameBoard[boardRow + i, boardColumn] = BOAT_SPACE_RIGHT_EDGE;
            }
        }

        public void ChangeOrientationAndShowBoatOnBoard(Boat boat, int row, int rowOffset, int column, int columnOffset)
        {
            if (boat.Orientation == Orientation.Horizontal)
                EraseHorizontalBoat(boat, row, rowOffset, column, columnOffset);
            else
                EraseVerticalBoat(boat, row, rowOffset, column, columnOffset);
            boat.ChangeOrientation();
            ShowBoatOnBoard(boat, row, rowOffset, column, columnOffset);
        }

        public void ShowBoatOnBoard(Boat boat, int row, int rowOffset, int column, int columnOffset)
        {
            if (boat.Orientation == Orientation.Horizontal)
                ShowHorizontalBoat(boat, row, rowOffset, column, columnOffset);
            else
                ShowVerticalBoat(boat, row, rowOffset, column, columnOffset);
        }

        public void ShowHorizontalBoat(Boat boat, int row, int rowOffset, int column, int columnOffset)
        {
            EraseHorizontalBoat(boat, row, rowOffset, column, columnOffset);
            for (int i = 0; i < boat.BoatLives.Length; i++)
            {
                Display.WriteAt(Display.WriteUnderline("O"), row, column + i * 2);
            }
        }

        public void EraseHorizontalBoat(Boat boat, int row, int rowOffset, int column, int columnOffset)
        {
            int boardRow = row - ROW_OFFSET + 1;
            int boardColumn = TranslateConsoleGridToGameBoard(column);
            int boardOffset;
            if (columnOffset == 2) boardOffset = 1;
            else if (columnOffset == -2) boardOffset = -1;
            else boardOffset = 0;

            for (int i = 0; i < boat.BoatLives.Length; i++)
            {
                if (GameBoard[boardRow - rowOffset, boardColumn + i - boardOffset] == EMPTY_SPACE ||
                    GameBoard[boardRow - rowOffset, boardColumn + i - boardOffset] == EMPTY_SPACE_RIGHT_EDGE)
                    Display.WriteAt(Display.WriteUnderline(" "), row - rowOffset, column - columnOffset + (i * 2));
                if (GameBoard[boardRow - rowOffset, boardColumn + i - boardOffset] == BOAT_SPACE ||
                    GameBoard[boardRow - rowOffset, boardColumn + i - boardOffset] == BOAT_SPACE_RIGHT_EDGE)
                    Display.WriteAt(Display.WriteUnderline("O"), row - rowOffset, column - columnOffset + (i * 2));
            }
        }

        private void ShowVerticalBoat(Boat boat, int row, int rowOffset, int column, int columnOffset)
        {
            EraseVerticalBoat(boat, row, rowOffset, column, columnOffset);
            for (int i = 0; i < boat.BoatLives.Length; i++)
            {
                Display.WriteAt(Display.WriteUnderline("O"), row + i, column);
            }
        }

        public void EraseVerticalBoat(Boat boat, int row, int rowOffset, int column, int columnOffset)
        {
            int boardRow = row - ROW_OFFSET + 1;
            int boardColumn = TranslateConsoleGridToGameBoard(column);
            int boardColumnOffset;
            if (columnOffset == 2) boardColumnOffset = 1;
            else if (columnOffset == -2) boardColumnOffset = -1;
            else boardColumnOffset = 0;

            int boardRowOffset;
            if (rowOffset == 1) boardRowOffset = 1;
            else if (rowOffset == -1) boardRowOffset = -1;
            else boardRowOffset = 0;
            for (int i = 0; i < boat.BoatLives.Length; i++)
            {
                if (GameBoard[boardRow + i - boardRowOffset, boardColumn - boardColumnOffset] == EMPTY_SPACE ||
                    GameBoard[boardRow + i - boardRowOffset, boardColumn - boardColumnOffset] == EMPTY_SPACE_RIGHT_EDGE)
                    Display.WriteAt(Display.WriteUnderline(" "), row - rowOffset + i, column - columnOffset);

                if (GameBoard[boardRow + i - boardRowOffset, boardColumn - boardColumnOffset] == BOAT_SPACE ||
                    GameBoard[boardRow + i - boardRowOffset, boardColumn - boardColumnOffset] == BOAT_SPACE_RIGHT_EDGE)
                    Display.WriteAt(Display.WriteUnderline("O"), row - rowOffset + i, column - columnOffset);
            }
        }

        private int TranslateConsoleGridToGameBoard(int column)
        {
            column = column - COLUMN_OFFSET;
            if (column == 0) return column + 1;

            int translator = 0;
            for (int i = 2; i < column; i += 2) 
            {
                translator++;
            }
            return column - translator;
        }

        public bool AreValidAttackCoordinates(Coordinate coordinates)
        {
            string valueAtCoordinates = GameBoard[coordinates.Row, coordinates.Column];
            if (valueAtCoordinates == EMPTY_SPACE ||
                valueAtCoordinates == EMPTY_SPACE_RIGHT_EDGE||
                valueAtCoordinates == BOAT_SPACE ||
                valueAtCoordinates == BOAT_SPACE_RIGHT_EDGE)
                return true;
            else
                return false;
        }

        public void AddAttack(Coordinate coordinates)
        {
            string valueAtCoordinates = GameBoard[coordinates.Row, coordinates.Column];
            if (valueAtCoordinates == EMPTY_SPACE)
            {
                GameBoard[coordinates.Row, coordinates.Column] = MISS_SPACE;
                Display.DisplayMiss();
            }
            else if (valueAtCoordinates == EMPTY_SPACE_RIGHT_EDGE)
            {
                GameBoard[coordinates.Row, coordinates.Column] = MISS_SPACE_RIGHT_EDGE;
                Display.DisplayMiss();
            }
            else if (valueAtCoordinates == BOAT_SPACE)
            {
                GameBoard[coordinates.Row, coordinates.Column] = HIT_BOAT_SPACE;
                Lives--;
                Display.DisplayHit();
            }
            else if (valueAtCoordinates == BOAT_SPACE_RIGHT_EDGE)
            {
                GameBoard[coordinates.Row, coordinates.Column] = HIT_BOAT_SPACE_RIGHT_EDGE;
                Lives--;
                Display.DisplayHit();
            }
        }
    }
}
