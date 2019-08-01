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
        public string PlayerName;// equivalent to { get; set; }
        public double Money = 100; //Amount of money given to a player
        public List<Card> Hand = new List<Card>();
        public int BlindPosition;

        public Player(string playerName)
        {
            PlayerName = playerName;
        }

        //Prints money
        public void PrintMoney()
        {
            Console.WriteLine("Money: {0:c}", Money);
        }

        //Prints hand
        public void PrintHand()
        {
            foreach (Card h in Hand)
                Console.WriteLine(h.ToString());
        }
        //move decision and enum into different files 

        public abstract Decision PerformTurn();
    }
}



   
