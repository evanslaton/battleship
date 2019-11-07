using System;
using System.Collections.Generic;
using System.Text;

namespace battleship
{
    public interface ITakeTurnBehavior
    {
        public void TakeTurn(Player opponent);
    }
}
