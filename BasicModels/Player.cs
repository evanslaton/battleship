using System;
using System.Collections.Generic;
using System.Text;

namespace battleship
{
    public enum PlayerType { Human, Computer }

    public class Player
    {
        public Board Board { get; set; }
        public Player Opponent { get; set; }
        IPlaceBoatBehavior BoatPlacer { get; set; }
        ITakeTurnBehavior TurnTaker {get; set;}

        public Player(IPlaceBoatBehavior boatPlacer, ITakeTurnBehavior turnTaker)
        {
            Board = new Board();
            Opponent = null;
            BoatPlacer = boatPlacer;
            TurnTaker = turnTaker;
        }

        public void PerformPlaceBoat()
        {
            BoatPlacer.PlaceBoat();
        }

        public void PerformTakeTurn()
        {
            TurnTaker.TakeTurn();
        }
    }
}
