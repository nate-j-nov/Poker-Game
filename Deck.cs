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
        public Stack<Card> deck = new Stack<Card>();

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
    
       public void PrintDeck()
       {
           foreach(var v in deck)
           {
               Console.WriteLine(v.ToString());
           }
       }
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

        public int CountCards()
        {
            return deck.Count;
        }    
    }
}









