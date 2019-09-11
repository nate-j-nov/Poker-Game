using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using static PokerGame.Dealer;
using static PokerGame.Card;
using static PokerGame.Round;
using static PokerGame.Decision;
using static PokerGame.PokerHand;

namespace PokerGame
{
    public abstract class Player
    {
        public string PlayerName; // equivalent to { get; set; }
        public double Money = 100; //Amount of money given to a player at beginning
        public PokerHand Hand = new PokerHand();
        public double raiseAmount;
        //public int BlindPosition; <- irrelevant

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
        public void PrintHand(IEnumerable<Card> commCards)
        {
            var sortedHand = Hand.ToList();
            sortedHand.AddRange(commCards.ToList());
            sortedHand = sortedHand.OrderByDescending(card => card.Face).ToList();
            foreach(var c in sortedHand)
            {
                Console.WriteLine(c.ToString());
            }
            
        }
       
        //Verify's input from the player. 
        //class due to its role in testing ExecuteTurn() found in Round.cs
        public abstract bool VerifyDecision(); 

        public abstract Decision PerformTurn();

        //Print TotalCards in the player's hand and what's in the community cards.
        //Used for testing purposes
        public void PrintTotalCards(IEnumerable<Card> communityCards)
        {
            foreach (var h in Hand)
            {
                Console.WriteLine(h);
            }

            foreach(var c in communityCards)
            {
                Console.WriteLine(c);
            }
        }
    }
}
