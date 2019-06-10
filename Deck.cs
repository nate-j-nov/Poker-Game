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
       public void Shuffle(Stack<Card> d) 
       {
           Random r = new Random();

           for(int x = 0; x < 25; x++)
           {
               int k = r.Next(x+1);
               Card temp = d.ElementAt(r);
               d.ElementAt(r) = d.ElementAt(x);
               d.ElementAt(x) = temp;
           }
       }
    }
}









