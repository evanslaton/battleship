using System;
using System.Collections.Generic;
using System.Text;

namespace battleship.PlaceBoatBehaviors
{
    public class HumanBoatPlacer : IPlaceBoatBehavior
    {
        public void PlaceBoat()
        {
            Console.WriteLine("Human placing a boat");
        }
    }
}
