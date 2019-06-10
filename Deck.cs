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
           for(int x = 0; x < d.Count; x++);
           /*I'm thinking that I want to take a random number (r) between 27 and 52
           and then take the card[x] and switch it with card[r] */
       }
    }
}









