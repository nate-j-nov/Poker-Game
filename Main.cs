using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using static PokerGame.Card;
using static PokerGame.Round;
using static PokerGame.Dealer;
using static PokerGame.CardSuit;
using static PokerGame.CardFace;
using static PokerGame.HumanPlayer;
using PokerGame.Extensions;
using static PokerGame.Game;

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







