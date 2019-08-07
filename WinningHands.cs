using System;

namespace PokerGame
{
    //Not critical for the code currently. Was going to create an enum of cards 
    public enum WinningHands
    {
        HighCard = 1,
        Pair,
        TwoPair,
        ThreeOfAKind,
        Straight,
        Flush,
        FullHouse,
        FourOfAKind,
        StraightFlush,
        RoyalFlush
    }
}
