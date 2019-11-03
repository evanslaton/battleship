using battleship.PlaceBoatBehaviors;
using battleship.TakeTurnBehaviors;
using System;
using System.Collections.Generic;
using System.Text;

namespace battleship
{
    public sealed class SimplePlayerFactory
    {
        private static readonly SimplePlayerFactory INSTANCE = new SimplePlayerFactory();
        public static SimplePlayerFactory Instance
        {
            get => INSTANCE;
        }

        private SimplePlayerFactory() { }

        public Player CreatePlayer(PlayerType playerType)
        {
            if (playerType == PlayerType.Human)
                return new Player(new HumanBoatPlacer(), new HumanTurnTaker());
            else
                return new Player(new ComputerBoatPlacer(), new ComputerTurnTaker());
        }
    }
}
