using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace battleship.TakeTurnBehaviors
{
    class ComputerTurnTaker : ITakeTurnBehavior
    {
        public void TakeTurn(Player opponent)
        {
            Display.DisplayHumanPlayerBoard(opponent);
            Random randomNumber = new Random();
            int row, column;

            do
            {
                row = randomNumber.Next(1, Board.BOARD_DIMENSION);
                column = randomNumber.Next(1, Board.BOARD_DIMENSION);
            } while (!opponent.Board.AreValidComputerAttackCoordinates(row, column));

            Thread.Sleep(2000);

            opponent.Board.AddComputerAttack(row, column);
        }
    }
}
