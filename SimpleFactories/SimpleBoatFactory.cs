using System;
using System.Collections.Generic;
using System.Text;

namespace battleship
{
    public sealed class SimpleBoatFactory
    {
        public static readonly SimpleBoatFactory INSTANCE = new SimpleBoatFactory();
        private static readonly Dictionary<BoatType, int> BOAT_LIVES = GetBoatLives();
        public static SimpleBoatFactory Instance
        {
            get => INSTANCE;
        }

        private SimpleBoatFactory() { }

        private static Dictionary<BoatType, int> GetBoatLives()
        {
            Dictionary<BoatType, int> boatLives = new Dictionary<BoatType, int>();
            boatLives.Add(BoatType.Destoyer, 1);
            boatLives.Add(BoatType.Submarine, 1);
            boatLives.Add(BoatType.Cruiser, 1);
            boatLives.Add(BoatType.Battleship, 1);
            boatLives.Add(BoatType.Carrier, 1);
            return boatLives;
        }

        public Boat CreateBoat(BoatType boatType) => new Boat(boatType, BOAT_LIVES[boatType]);

    }
}
