using System;
using System.Collections.Generic;
using System.Text;

namespace battleship
{
    public enum Orientation { Horizontal, Vertical }
    public enum BoatType { Destoyer, Submarine, Cruiser, Battleship, Carrier }


    public class Boat
    {
        public BoatType BoatType { get; set; }
        public bool[] BoatLives { get; set; }
        public bool IsSunk { get; set; }
        public Orientation Orientation { get; set; }

        public Boat(BoatType boatType, int lives)
        {
            BoatType = boatType;
            BoatLives = new bool[lives];
            IsSunk = false;
            Orientation = Orientation.Horizontal;
        }

        public void ChangeOrientation()
        {
            if (Orientation == Orientation.Horizontal) Orientation = Orientation.Vertical;
            else Orientation = Orientation.Horizontal;
        }
    }
}
