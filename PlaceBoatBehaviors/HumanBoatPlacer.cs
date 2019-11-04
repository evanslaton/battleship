using System;
using System.Collections.Generic;
using System.Text;

namespace battleship.PlaceBoatBehaviors
{
    public class HumanBoatPlacer : IPlaceBoatBehavior
    {
        public void PlaceBoat()
        {
            int boatTypes = Enum.GetValues(typeof(BoatType)).Length;
            Console.WriteLine(boatTypes);
            ConsoleKeyInfo userInput;
            bool selectionWasMade;
            Boat boat;
            int row;
            int column;

            for (int i = 0; i < boatTypes; i++)
            {
                selectionWasMade = false;
                boat = SimpleBoatFactory.INSTANCE.CreateBoat((BoatType)i);
                row = Board.ROW_OFFSET;
                column = Board.COLUMN_OFFSET;
                Display.WriteAt(Display.WriteUnderline("X"), row, column);

                while (!selectionWasMade)
                {
                    userInput = Console.ReadKey(true);
                    if (userInput.Key == ConsoleKey.Enter)
                    {
                        selectionWasMade = true;
                    }
                    else if (userInput.Key == ConsoleKey.LeftArrow && column > Board.COLUMN_OFFSET)
                    {
                        Display.WriteAt(Display.WriteUnderline(" "), row, column);
                        column -= 2;
                    }
                    else if (userInput.Key == ConsoleKey.UpArrow && row > Board.ROW_OFFSET)
                    {
                        Display.WriteAt(Display.WriteUnderline(" "), row, column);
                        row--;
                    }
                    else if (userInput.Key == ConsoleKey.RightArrow && column < Board.COLUMN_OFFSET + 18)
                    {
                        Display.WriteAt(Display.WriteUnderline(" "), row, column);
                        column += 2;
                    }
                    else if (userInput.Key == ConsoleKey.DownArrow && row < Board.ROW_OFFSET + 9)
                    {
                        Display.WriteAt(Display.WriteUnderline(" "), row, column);
                        row++;
                    }
                    else if (userInput.Key == ConsoleKey.Spacebar)
                    {
                        boat.ChangeOrientation();
                    }
                    Display.WriteAt(Display.WriteUnderline("X"), row, column);
                }
            }
        }
    }
}
