using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using static PokerGame.CardFace;
using static PokerGame.CardSuit;

namespace PokerGame
{
    public sealed class Card
    {
        //Initializes and retrieves card's face and suit
        public CardFace Face { get; }
        public CardSuit Suit { get; }

        //Creates a card 
        public Card(CardFace face, CardSuit suit)
        {
            Face = face;
            Suit = suit;
        }

        //Prints the card
        public override string ToString()
        {
            return Face + " of " + Suit;
        }

        //Checks value of a card
        public bool IsHigherThan(Card otherCard)
        {
            return Face >= otherCard.Face && Suit > otherCard.Suit;
        }
    }     
}
        


        