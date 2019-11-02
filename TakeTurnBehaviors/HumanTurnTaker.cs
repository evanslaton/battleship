using System;
using System.Collections.Generic;
using System.Text;

namespace battleship.TakeTurnBehaviors
{
    class HumanTurnTaker : ITakeTurnBehavior
    {
        public void TakeTurn()
        {
            Console.WriteLine("Human turn");
        }
    }
}
