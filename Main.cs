using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using static PokerGame.Card;
using static PokerGame.Deck;
using static PokerGame.CardFace;
using static PokerGame.CardSuit;
using static PokerGame.HumanPlayer;
using static PokerGame.CommunityCards;

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

            //Create deck
            Deck d = new Deck();
            d.Shuffle();

            //deal all commuity cards. This is a test.
            CommunityCards cc = new CommunityCards(d);
            cc.DrawTurn(d);
            cc.DrawRiver(d);

        }
    }
}







