using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace battleship.TakeTurnBehaviors
{
    class HumanTurnTaker : ITakeTurnBehavior
    {
        public void TakeTurn(Player opponent)
        {
            Board OpponentBoard = opponent.Board;
            Coordinate coordinate;
            bool isValidAttack;
            do
            {
                coordinate = GetAttackCoordinates();
                isValidAttack = OpponentBoard.AreValidAttackCoordinates(coordinate);

            } while (!isValidAttack);
            OpponentBoard.AddAttack(coordinate);
        }

        private Coordinate GetAttackCoordinates()
        {
            string userInput;
            Match validInput;
            string REGEX = @"^((([1-9]|10)[a-j])|(([a-j]([1-9]|10))))$";
            bool isValidInput = false;
            int COORDINATES_LINE = 11;
            int COORDINATES_LINE_LENGTH = 18;
            do
            {
                Console.SetCursorPosition(0, COORDINATES_LINE);
                Console.Write(new string(' ', Console.WindowWidth));
                Console.SetCursorPosition(0, COORDINATES_LINE);
                Display.CenterTextWithoutReturn("Coordinates: ", COORDINATES_LINE_LENGTH);
                userInput = Console.ReadLine();
                userInput = userInput.ToLower();
                validInput = Regex.Match(userInput, REGEX);
                isValidInput = validInput.Success;
            } while (!isValidInput);
            return new Coordinate(userInput);
        }
    }
}
