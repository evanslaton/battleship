using System;
using System.Collections.Generic;
using System.Text;

namespace battleship.PlaceBoatBehaviors
{
    class ComputerBoatPlacer : IPlaceBoatBehavior
    {
        public void PlaceBoat(Board board)
        {
            Random randomNumber = new Random();
            int numberOfBoats = Enum.GetValues(typeof(BoatType)).Length;
            Boat boat;
            int boatLength, row, column, orientation;

            for (int i = 0; i < numberOfBoats; i++)
            {
                boat = SimpleBoatFactory.INSTANCE.CreateBoat((BoatType)i);
                boatLength = boat.BoatLives.Length;
                do
                {
                    orientation = randomNumber.Next(0, 2);
                    boat.Orientation = (Orientation)orientation;

                    if (boat.Orientation == Orientation.Horizontal)
                    {
                        row = randomNumber.Next(1, Board.BOARD_DIMENSION);
                        column = randomNumber.Next(1, Board.BOARD_DIMENSION + 1 - boatLength);
                    }
                    else
                    {
                        row = randomNumber.Next(1, Board.BOARD_DIMENSION + 1 - boatLength);
                        column = randomNumber.Next(1, Board.BOARD_DIMENSION);
                    }
                } while (!board.AddComputerBoatToBoard(boat, row, column));
            }
        }
    }
}
