using System;
using System.Collections.Generic;

namespace PokerGame
{
    class Program
    { 
        static void Main(string[] args)
        {       
            Console.WriteLine("Welcome to poker! The rules are standard Texas Holdem'." + Environment.NewLine +
                "If you run out of money or you let your loan expire past 5 rounds," + Environment.NewLine +
                "you'll lose and the game will end." + Environment.NewLine +
                "Have fun!" + Environment.NewLine + "Press Enter to begin the game.");

            Console.ReadLine();

            var playersInGame = new List<Player>()
            {
                new HumanPlayer("Nate"),
                new ComputerPlayer("Jake"),
                new ComputerPlayer("Evan"),
                new ComputerPlayer("Chad")
            };

            var game = new Game(playersInGame);
            foreach(var p in playersInGame)
            {
                if(p is HumanPlayer)
                {
                    game.HumanPlayingGame = (HumanPlayer)p;
                }
            }
            game.RunGame();
        }
    }
}







