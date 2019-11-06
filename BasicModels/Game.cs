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
        public NumberOfHumanPlayers NumberOfHumanPlayers { get; private set; }

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

            Display.DisplayBoatPlacing(game);
            game.ActivePlayer.PerformPlaceBoat();
            game.ChangeActivePlayer();

            if (game.NumberOfHumanPlayers == NumberOfHumanPlayers.TwoPlayers)
                Display.DisplayBoatPlacing(game);
            game.ActivePlayer.PerformPlaceBoat();
            game.ChangeActivePlayer();

            bool activePlayerHasWon = false;
            while (!activePlayerHasWon)
            {
                Display.DisplayOpponentBoard(game);
                game.ActivePlayer.PerformTakeTurn();
                activePlayerHasWon = game.CheckForWin();
                if (!activePlayerHasWon) game.ChangeActivePlayer();
            }

            Display.DisplayWinner(game);
            Console.ReadKey();
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
            CreateOtherPlayer();
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

        private bool CheckForWin() => ActivePlayer.Opponent.Board.Lives == 0;
    }
}
