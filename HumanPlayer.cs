using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using static PokerGame.Deck;
using static PokerGame.Card;

namespace PokerGame
{
    public class HumanPlayer
    {
        public string PlayerName { get; }
        public double Money = 100; //Amount of money given to a player
        private Card[] Hand = new Card[2];
        public double OtherPlyrBet;
        const double BigBlind = 2.00;
        const double SmallBlind = 1.00;
        public double Pot;
       
        //HumanPlayer constructor taking string name and deck. 
        public HumanPlayer(string nm, Deck d)
        {
            Console.WriteLine();
            PlayerName = nm;
            for (int i = 0; i < 2; i++)
            {
                Hand[i] = d.DrawCard();
            }
        }

        //Prints money
        public void PrintMoney()
        {
            Console.WriteLine("Money: {0:c}", Money);
        }

        //Prints hand
        public void PrintHand()
        {
            foreach (var v in Hand)
                Console.WriteLine(v.ToString());
        }

        //Match either other players' bet or the big blind. 
        public void Call()
        {
            //code to read in Money on the table
        }

        //Raises money in the pot
        public void Raise()
        {
            Console.WriteLine($"How much would you like to bet, {PlayerName}?");

            string Entered = Console.ReadLine();
            double RaiseAmt = Convert.ToDouble(Entered);

            if (RaiseAmt <= Money && RaiseAmt > 0)
            {
                Money -= RaiseAmt;
                Console.WriteLine("You raise {0:c}.", RaiseAmt);
                Pot += RaiseAmt;
                PrintMoney();
            }
            else
            {
                Console.WriteLine("You can't afford to bet that, " +
                    "but you can borrow from the loan shark if you'd like");
            }
        }

        //Fold function, return true. If Fold = TRUE, round ends
        public bool Fold()
        {
            return true;
        }
    }
}
