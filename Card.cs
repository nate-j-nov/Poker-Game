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
        public CardFace Face { get; }
        public CardSuit Suit { get; }

        public Card(CardFace face, CardSuit suit)
        {
            Face = face;
            Suit = suit;
        }

        public override string ToString()
        {
            return Face + " of " + Suit;
        }
        public bool IsHigherThan(Card otherCard)
        {
            return Face >= otherCard.Face && Suit > otherCard.Suit;
        }
    }     
}
        


        