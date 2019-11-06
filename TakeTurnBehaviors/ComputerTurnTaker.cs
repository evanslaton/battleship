using System;
using System.Collections.Generic;
using System.Text;

namespace battleship.TakeTurnBehaviors
{
    class ComputerTurnTaker : ITakeTurnBehavior
    {
        public void TakeTurn(Player opponent)
        {
            Console.WriteLine("Computer turn");
        }
    }
}
