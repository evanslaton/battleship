using System;
using System.Collections.Generic;
using System.Text;

namespace battleship
{
    public enum PlayerType { Human, Computer }

    public class Player
    {
        public string Name { get; set; }
        public Board Board { get; set; }
        public Player Opponent { get; set; }
        IPlaceBoatBehavior BoatPlacer { get; set; }
        ITakeTurnBehavior TurnTaker {get; set;}

        public Player(string name, IPlaceBoatBehavior boatPlacer, ITakeTurnBehavior turnTaker)
        {
            Name = name;
            Board = new Board();
            Opponent = null;
            BoatPlacer = boatPlacer;
            TurnTaker = turnTaker;
        }

        public void PerformPlaceBoat()
        {
            BoatPlacer.PlaceBoat(Board);
        }

        public void PerformTakeTurn(Game game)
        {
            TurnTaker.TakeTurn(Opponent);
        }
    }
}
