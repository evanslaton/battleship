using System;
using System.Collections.Generic;
using System.Text;

namespace battleship
{
    public enum Orientation { Horizontal, Vertical }
    public enum BoatType { Destoyer, Submarine, Cruiser, Battleship, Carrier }

    public class Boat
    {
        BoatType BoatType { get; set; }
        bool[] BoatLives { get; set; }
        bool IsSunk { get; set; }
        Orientation Orientation { get; set; }

        public Boat(BoatType boatType, int lives)
        {
            BoatType = boatType;
            BoatLives = new bool[lives];
            IsSunk = false;
            Orientation = Orientation.Horizontal;
        }
    }
}
