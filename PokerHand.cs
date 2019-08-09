using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace PokerGame
{
    public class PokerHand : List<Card>
    {
        private List<Card> _cards = new List<Card>();

        public WinningHands GetBestHand(IEnumerable<Card> communityCards)
        {
            var handCards = _cards.ToList();
            handCards.AddRange(communityCards);

            // Evaluate my hand.


            return WinningHands.Flush;
        }
    }
}