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
        public Boat[] Boats { get; set; } 
        public string[,] GameBoard { get; set; }
        public SimpleBoatFactory SimpleBoatFactory { get; }

        public Board()
        {
            GameBoard = new string[BOARD_DIMENSION, BOARD_DIMENSION];
            FillBoard();
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
                GameBoard[0, column + 1] = $" {WriteUnderline(COLUMN_HEADERS[column])}";
            }
        }

        private void SetCells()
        {
            for (int row = 1; row < BOARD_DIMENSION; row++)
            {
                for (int column = 1; column < BOARD_DIMENSION; column++)
                {
                    if (column == BOARD_DIMENSION - 1) GameBoard[row, column] = $"|{WriteUnderline(" ")}|";
                    else GameBoard[row, column] = $"|{WriteUnderline(" ")}";
                }
            }
        }

        private static string WriteUnderline(string stringToUnderline)
        {
            var handle = GetStdHandle(STD_OUTPUT_HANDLE);
            uint mode;
            GetConsoleMode(handle, out mode);
            mode |= ENABLE_VIRTUAL_TERMINAL_PROCESSING;
            SetConsoleMode(handle, mode);
            return $"\x1B[4m{stringToUnderline}\x1B[24m";
        }
    }
}
