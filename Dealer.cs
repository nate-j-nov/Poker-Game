using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using System.Linq;
using static PokerGame.Card;
using static PokerGame.CardFace;
namespace PokerGame
{
    public sealed class Dealer
    {
        private Stack<Card> deck = new Stack<Card>();
        public List<Card> CommCards = new List<Card>(); //Declares Community Cards

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

        //Counts cards (For testing purposes, not illicit purposes)
        public int CountCards()
        {
            return deck.Count;
        }

        public void DealPlayerCards()
        {
            List<Card> playerCards = new List<Card>();
            for (int x = 0; x < 2; x++)
                playerCards.Add(DrawCard());
        }

        //draws card from deck
        public Card DrawCard()
        {
            return deck.Pop();
        }

        //Draws first three cards of Community Cards
        public void DrawFlop()
        {
            for (int i = 0; i < 3; i++)
            {
                CommCards.Add(DrawCard());
            }
            PrintCommCards();
        }

        //Draws fourth community card
        public void DrawTurn()
        {
            if (CommCards.Count == 3)
            {
                CommCards.Add(DrawCard());
                PrintCommCards();
            }
            else
            {
                Console.WriteLine("Can't do this");
            }
        }

        //Draws fifth community card
        public void DrawRiver()
        {
            if (CommCards.Count == 4)
            {
                CommCards.Add(DrawCard());
                PrintCommCards();
            }
            else
            {
                Console.WriteLine("Can't do this");
            }
        }

        //Prints community cards
        public void PrintCommCards()
        {
            foreach (Card e in CommCards)
                Console.Write("  {0}  ", e.ToString());
            Console.WriteLine();
        }
    }
}










