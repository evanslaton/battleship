using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace battleship
{
    public enum NumberOfHumanPlayers { OnePlayer, TwoPlayers }

    public class Game
    {
        private static string[] PLAYER_NAMES = { "Player 1", "Player 2" };
        private SimplePlayerFactory SimplePlayerFactory { get; set; }
        public Player ActivePlayer { get; set; }
        public Player InactivePlayer { get; set; }
        private NumberOfHumanPlayers NumberOfHumanPlayers { get; set; }

        public Game()
        {
            SimplePlayerFactory = SimplePlayerFactory.Instance;
            ActivePlayer = SimplePlayerFactory.CreatePlayer(PLAYER_NAMES[0], PlayerType.Human);
            InactivePlayer = null;
            NumberOfHumanPlayers = NumberOfHumanPlayers.OnePlayer;
        }

        public static void Start()
        {
            Game game = new Game();
            Display.DisplayWelcome();
            game.SelectNumberOfPlayers();
            game.CreateOtherPlayer();

            Display.DisplayBoatPlacing(game);
            game.ActivePlayer.PerformPlaceBoat();
            game.ChangeActivePlayer();

            Display.DisplayBoatPlacing(game);
            game.ActivePlayer.PerformPlaceBoat();
            game.ChangeActivePlayer();

            while (true)
            {
                Display.DisplayOpponentBoard(game);
                game.Attack();

                Display.DisplayOpponentBoard(game);
                Console.ReadLine();
                game.ChangeActivePlayer();
            }
        }

        private void Attack()
        {
            Coordinate coordinate = GetAttackCoordinates();
            Console.WriteLine(coordinate.GameBoardRow + ", " + coordinate.GameBoardColumn);

            Board OpponentBoard = ActivePlayer.Opponent.Board;
            bool isValidAttack = OpponentBoard.AreValidAttackCoordinates(coordinate);
            if (isValidAttack)
            {
                Console.WriteLine("VALID");
                OpponentBoard.AddAttack(coordinate);
            }
            else
                Console.WriteLine("INVALID ATTACK");
        }

        private Coordinate GetAttackCoordinates()
        {
            string userInput;
            Match validInput;
            string REGEX = @"^((([1-9]|10)[a-j])|(([a-j]([1-9]|10))))$";
            bool isValidInput = false;
            do
            {
                userInput = Console.ReadLine();
                userInput = userInput.ToLower();
                validInput = Regex.Match(userInput, REGEX);
                isValidInput = validInput.Success;
            } while (!isValidInput);
            return new Coordinate(userInput);
        }

        private void SelectNumberOfPlayers()
        {
            ConsoleKeyInfo userInput;
            bool selectionWasMade = false;
            while (!selectionWasMade)
            {
                userInput = Console.ReadKey(true);
                if (userInput.Key == ConsoleKey.Enter)
                    selectionWasMade = true;
                else if (userInput.Key == ConsoleKey.LeftArrow)
                    NumberOfHumanPlayers = NumberOfHumanPlayers.OnePlayer;
                else if (userInput.Key == ConsoleKey.RightArrow)
                    NumberOfHumanPlayers = NumberOfHumanPlayers.TwoPlayers;
                Display.DisplayCurrentlySelectedNumberOfPlayers(NumberOfHumanPlayers);
            }
        }

        private void CreateOtherPlayer()
        {
            if (NumberOfHumanPlayers == NumberOfHumanPlayers.OnePlayer)
                InactivePlayer = SimplePlayerFactory.CreatePlayer(PLAYER_NAMES[1], PlayerType.Computer);
            else
                InactivePlayer = SimplePlayerFactory.CreatePlayer(PLAYER_NAMES[1], PlayerType.Human);
            ActivePlayer.Opponent = InactivePlayer;
            InactivePlayer.Opponent = ActivePlayer;
        }

        public void ChangeActivePlayer()
        {
            Player temporary = ActivePlayer;
            ActivePlayer = InactivePlayer;
            InactivePlayer = temporary;
        }
    }
}
