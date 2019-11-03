using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace battleship
{
    public enum NumberOfHumanPlayers { OnePlayer, TwoPlayers }

    public class Game
    {
        private Player ActivePlayer { get; set; }
        private Player InactivePlayer { get; set; }
        private NumberOfHumanPlayers NumberOfHumanPlayers { get; set; }
        private SimplePlayerFactory SimplePlayerFactory { get; set; }

        public Game()
        {
            ActivePlayer = null;
            InactivePlayer = null;
            NumberOfHumanPlayers = NumberOfHumanPlayers.OnePlayer;
            SimplePlayerFactory = SimplePlayerFactory.Instance;
        }

        public static void Start()
        {
            Display.DisplayWelcome();
            Game game = new Game();
            game.SelectNumberOfPlayers();
            game.CreatePlayers();

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

        private void CreatePlayers()
        {
            ActivePlayer = SimplePlayerFactory.CreatePlayer(PlayerType.Human);
            if (NumberOfHumanPlayers == NumberOfHumanPlayers.OnePlayer)
                InactivePlayer = SimplePlayerFactory.CreatePlayer(PlayerType.Computer);
            else
                SimplePlayerFactory.CreatePlayer(PlayerType.Human);
        }
    }
}
