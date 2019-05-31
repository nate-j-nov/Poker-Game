using System;
using System.Collections.Generic;

namespace PokerGame
{

    //public sealed Stack<T> Deck()
    /*class Card
    {
        Card(string face, string suit);
    }*/

    public class Deck
    {
        string[] faces = {"Ace", "Two", "Three", "Four", "Five", "Six", "Seven", "Eight",
            "Nine", "Ten", "Jack", "Queen", "King"};
        string[] suit = { "Hearts", "Diamonds", "Spades", "Clubs" };

        public Deck()
        {
            foreach (string str in suit)
            {
                foreach (string k in faces)
                {
                    Console.WriteLine(k + " of " + str);

                }
            }
        }
    }
   
    class Program
    {
        static void Main(string[] args)
        {
            Deck d = new Deck();
        }
    }
}

