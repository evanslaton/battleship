using System;
using System.Collections.Generic;
using System.Text;

namespace battleship.TakeTurnBehaviors
{
    class ComputerTurnTaker : ITakeTurnBehavior
    {
        public void TakeTurn()
        {
            Console.WriteLine("Computer turn");
        }
    }
}
