using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using static PokerGame.Dealer;
using static PokerGame.Card;
using static PokerGame.Round;
using static PokerGame.Decision;
using PokerGame;

namespace PokerGame
{

    public abstract class Player
    {
        public string PlayerName; // equivalent to { get; set; }
        public double Money = 100; //Amount of money given to a player at beginning
        public List<Card> Hand = new List<Card>();
        //public int BlindPosition; <- irrelevant

        /*List of cards that are in the players hand or in the pocket community
         cards. This is part of my idea of how to determine the winning hands of each
         player. Ex: Royal Flush, straight, three of a kind etc.*/
        private List<Card> TotalCards = new List<Card>(); 

        public Player(string playerName)
        {
            PlayerName = playerName;
        }

        //Prints player's money
        public void PrintMoney()
        {
            Console.WriteLine("Money: {0:c}", Money);
        }

        //Prints player's hand
        public void PrintHand()
        {
            foreach (Card h in Hand)
                Console.WriteLine(h.ToString());
        }
       
        //Verify's input from the player. 
        //class due to its role in testing ExecuteTurn() found in Round.cs
        public abstract bool VerifyDecision(); 

        public abstract Decision PerformTurn();

        //Method to fill TotalCards, which is a list of cards that is used to
        //read the hands and community cards to determine the winner
        public void PopulateTotalCards(List<Card> commCards)
        {
            foreach (var h in Hand)
                TotalCards.Add(h);

            foreach (var c in commCards)
                TotalCards.Add(c);
        }

        //Print TotalCards in the player's hand and what's in the community cards.
        //Used for testing purposes
        public void PrintTotalCards()
        {
            foreach (var t in TotalCards)
            {
                Console.WriteLine(t.ToString());
            }
        }
    }
}



   
