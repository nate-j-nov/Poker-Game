using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using System.Linq;

using static PokerGame.Card;
using static PokerGame.CardFace;
namespace PokerGame
{   
    public sealed class Deck
    {
        private Stack<Card> deck = new Stack<Card>();

        //create deck
        public Deck()
        {
            foreach (CardSuit s in Enum.GetValues(typeof(CardSuit)))
            {
                foreach (CardFace f in Enum.GetValues(typeof(CardFace)))
                {
                    Card c = new Card(f, s);
                    deck.Push(c);
                }
            }
        }

        //Prints deck
       public void PrintDeck()
        {
           foreach(var v in deck)
            {
               Console.WriteLine(v.ToString());
            }
        }

        //Shuffles deck
        public void Shuffle()
        {
            Random r = new Random();

            Card[] arrOfCards = deck.ToArray();
            deck.Clear();
            for (int x = arrOfCards.Length - 1; x > 0; --x)
            {
                int k = r.Next(x + 1);
                var temp = arrOfCards[x];
                arrOfCards[x] = arrOfCards[k];
                arrOfCards[k] = temp;
            }
            foreach (var x in arrOfCards)
            {
                deck.Push(x);
            }
        }

        //Counts cards (For testing purposes, not illicit purposes)
        public int CountCards()
        {
            return deck.Count;
        } 

        //draws card from deck
        public Card DrawCard()
        {
            return deck.Pop();
        }
    }
}









