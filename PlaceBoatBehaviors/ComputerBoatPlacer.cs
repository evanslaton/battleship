﻿using System;
using System.Collections.Generic;
using System.Text;

namespace battleship.PlaceBoatBehaviors
{
    class ComputerBoatPlacer : IPlaceBoatBehavior
    {
        public void PlaceBoat(Board board)
        {
            Console.WriteLine("Computer placing a boat");
        }
    }
}
