using System;
using System.Collections.Generic;

namespace PokerGame
{
    class Program
    { 
        static void Main(string[] args)
        {       
            Console.WriteLine("Welcome to poker!");

            var playersInGame = new List<Player>()
            {
                new HumanPlayer("Nate"),
                new ComputerPlayer("Jake"),
                new ComputerPlayer("Evan"),
                new ComputerPlayer("Chad")
            };

            var game = new Game(playersInGame);
            game.NextRound(2.0);
        }
    }
}







