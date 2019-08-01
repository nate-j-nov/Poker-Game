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



namespace PokerGame
{
    class Program
    { 
        static void Main(string[] args)
        {
            //Welcome
            Console.WriteLine("Hello! What's your name?");

            //Get name of human player
            string name = Console.ReadLine();
            Console.WriteLine($"Welcome, {name}!");

            Round round = new Round();

        }
    }
}







