using System;
using System.Collections.Generic;
using System.Text;

namespace battleship.PlaceBoatBehaviors
{
    public class HumanBoatPlacer : IPlaceBoatBehavior
    {
        public void PlaceBoat(Board board)
        {
            int numberOfBoats = Enum.GetValues(typeof(BoatType)).Length;
            ConsoleKeyInfo userInput;
            bool selectionWasMade;
            Boat boat;
            int row, column;

            for (int i = 0; i < numberOfBoats; i++)
            {
                selectionWasMade = false;
                boat = SimpleBoatFactory.INSTANCE.CreateBoat((BoatType)i);
                row = Board.ROW_OFFSET;
                column = Board.COLUMN_OFFSET;
                board.ShowBoatOnBoard(boat, row, 0, column, 0);

                while (!selectionWasMade)
                {
                    userInput = Console.ReadKey(true);
                    if (userInput.Key == ConsoleKey.Enter)
                    {
                        selectionWasMade = board.AddBoatToBoard(boat, row, column);
                    }
                    else if (userInput.Key == ConsoleKey.LeftArrow && column > Board.COLUMN_OFFSET)
                    {
                        board.ShowBoatOnBoard(boat, row, 0, column -= 2, -2);
                    }
                    else if (userInput.Key == ConsoleKey.UpArrow && row > Board.ROW_OFFSET)
                    {
                        board.ShowBoatOnBoard(boat, --row, -1, column, 0);
                    }
                    else if (userInput.Key == ConsoleKey.RightArrow && IsInRightBoundary(boat, column))
                    {
                        board.ShowBoatOnBoard(boat, row, 0, column += 2, 2);
                    }
                    else if (userInput.Key == ConsoleKey.DownArrow && IsInBottomBoundary(boat, row))
                    {
                        board.ShowBoatOnBoard(boat, ++row, 1, column, 0);
                    }
                    else if (userInput.Key == ConsoleKey.Spacebar)
                    {
                        if ((boat.Orientation == Orientation.Horizontal && WillBeInBottomBoundary(boat, row)) ||
                            (boat.Orientation == Orientation.Vertical && WillBeInRightBoundary(boat, column)))
                        {
                            board.ChangeOrientationAndShowBoatOnBoard(boat, row, 0, column, 0);
                        }
                    }
                }
            }
        }

        private static bool IsInRightBoundary(Boat boat, int column)
        {
            int HORIZONTAL_BOAT_RIGHT_BOUNDARY = Board.COLUMN_OFFSET + 20 - boat.BoatLives.Length * 2;
            int VERTICAL_BOAT_RIGHT_BOUNDARY = Board.COLUMN_OFFSET + 18;
            return boat.Orientation == Orientation.Horizontal && column < HORIZONTAL_BOAT_RIGHT_BOUNDARY ||
                        boat.Orientation == Orientation.Vertical && column < VERTICAL_BOAT_RIGHT_BOUNDARY;
        }

        private static bool IsInBottomBoundary(Boat boat, int row)
        {
            int HORIZONTAL_BOAT_BOTTOM_BOUNDARY = Board.ROW_OFFSET + 9;
            int VERTICAL_BOAT_BOTTOM_BOUNDARY = Board.ROW_OFFSET + 10 - boat.BoatLives.Length;
            return boat.Orientation == Orientation.Horizontal && row < HORIZONTAL_BOAT_BOTTOM_BOUNDARY ||
                       boat.Orientation == Orientation.Vertical && row < VERTICAL_BOAT_BOTTOM_BOUNDARY;
        }

        private static bool WillBeInRightBoundary(Boat boat, int column)
        {
            int HORIZONTAL_BOAT_RIGHT_BOUNDARY = Board.COLUMN_OFFSET + 21 - boat.BoatLives.Length * 2;
            return column < HORIZONTAL_BOAT_RIGHT_BOUNDARY;
        }

        private static bool WillBeInBottomBoundary(Boat boat, int row)
        {
            int VERTICAL_BOAT_BOTTOM_BOUNDARY = Board.ROW_OFFSET + 11 - boat.BoatLives.Length;
            return row < VERTICAL_BOAT_BOTTOM_BOUNDARY;
        }
    }
}
