using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;
namespace PokerGame
{

    class Program
    {
        public enum CardFace
        {
            Ace = 1,
            Two,
            Three,
            Four,
            Five,
            Six,
            Seven,
            Eight,
            Nine,
            Ten,
            Jack,
            Queen,
            King
        }

        public enum CardSuit
        {
            Clubs = 1, //Specified as one because assigning zero to it may cause
                       //complications when assigning scores to hands
            Diamonds,
            Hearts,
            Spades
        }
        public sealed class Card
        {
            public CardFace Face { get; }
            public CardSuit Suit { get; }

            public Card(CardFace face, CardSuit suit)
            {
                Face = face;
                Suit = suit;
            }

            public bool IsHigherThan(Card otherCard)
            {
                return Face >= otherCard.Face && Suit > otherCard.Suit;
            }
        }



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
                foreach (Card c in deck)
                {
                    Console.WriteLine(c);
                }
            }
            
            public void DrawCard()
            { 
                deck.Pop();
            }
        }

        static void Main(string[] args)
        {
            Deck d = new Deck();
            d.PrintDeck();
        }
    }
}

