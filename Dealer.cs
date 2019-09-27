using PokerGame.Enums;
using System;
using System.Collections.Generic;

namespace PokerGame
{
    public sealed class Dealer
    {
        private Stack<Card> deck = new Stack<Card>();

        public void PopulateDeck()
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
            foreach (var v in deck)
            {
                Console.WriteLine(v.ToString());
            }
        }

        //Shuffles deck
        public void ShuffleDeck()
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

        //Draws card from deck
        public Card DrawCard()
        {
            return deck.Pop();
        }

        //Deals hand to player
        public void DealPlayerCards(Player player)
        {
            for(int x = 0; x < 2; x++)
            {
                player.Hand.Add(DrawCard());
            }
        }
    }
}










