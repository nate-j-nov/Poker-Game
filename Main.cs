using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using static PokerGame.Card;
using static PokerGame.Deck;
using static PokerGame.CardFace;
using static PokerGame.CardSuit;

namespace PokerGame
{
    class Program
    { 
        static void Main(string[] args)
        {
                Deck d = new Deck();
                d.PrintDeck();
        }
    }
}







