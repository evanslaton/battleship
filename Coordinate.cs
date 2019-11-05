using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace battleship
{
    public class Coordinate
    {
        public static string[] LETTERS = { "a", "b", "c", "d", "e", "f", "g", "h", "i", "j" };
        public int GameBoardRow { get; }
        public int GameBoardColumn { get; }
        public int ConsoleBoardRow {get;}
        public int ConsoleBoardColumn { get; }

        public Coordinate(string coordinates)
        {
            GameBoardRow = ToGameBoardRow(coordinates);
            GameBoardColumn = ToGameBoardColumn(coordinates);
        }

        private static int ToGameBoardRow(string coordinates)
        {
            string FIND_NUMBERS = @"\d";
            string row = Regex.Replace(coordinates, FIND_NUMBERS, "");
            return Array.IndexOf(LETTERS, row) + 1;
        }

        private static int ToGameBoardColumn(string coordinates)
        {
            string FIND_LETTERS = @"[a-j]";
            string column = Regex.Replace(coordinates, FIND_LETTERS, "");
            return Int32.Parse(column);
        }
    }
}
