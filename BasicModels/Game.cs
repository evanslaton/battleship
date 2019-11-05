using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace battleship
{
    public enum NumberOfHumanPlayers { OnePlayer, TwoPlayers }

    public class Game
    {
        private SimplePlayerFactory SimplePlayerFactory { get; set; }
        public Player ActivePlayer { get; set; }
        public Player InactivePlayer { get; set; }
        private NumberOfHumanPlayers NumberOfHumanPlayers { get; set; }

        public Game()
        {
            SimplePlayerFactory = SimplePlayerFactory.Instance;
            ActivePlayer = SimplePlayerFactory.CreatePlayer(PlayerType.Human);
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

            Display.DisplayBoatPlacing(game);
            game.ActivePlayer.PerformPlaceBoat();
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
                InactivePlayer = SimplePlayerFactory.CreatePlayer(PlayerType.Computer);
            else
                InactivePlayer = SimplePlayerFactory.CreatePlayer(PlayerType.Human);
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
