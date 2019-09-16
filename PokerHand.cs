using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using static PokerGame.Player;

namespace PokerGame
{
    public class PokerHand : List<Card>
    {
        private List<Card> _cards = new List<Card>();
        public List<Card> Cards { get; set; }
        
        public WinningHands GetBestHand(IEnumerable<Card> communityCards)
        {
            _cards = Cards;
            var handCards = _cards;
            handCards.AddRange(communityCards);
           
            if (HasRoyalFlush(handCards))
            {
                return WinningHands.RoyalFlush;            
            }
            else if (HasStraightFlush(handCards))
            {
                return WinningHands.StraightFlush;
            }
            else if (HasFourOfAKind(handCards))
            {
                return WinningHands.FourOfAKind;
            }
            else if (HasFullHouse(handCards))
            {
                return WinningHands.FullHouse;
            }
            else if (HasFlush(handCards))
            {
                return WinningHands.Flush;
            }
            else if (HasStraight(handCards))
            { 
                return WinningHands.Straight;
            }
            else if (HasTrips(handCards))
            { 
                return WinningHands.ThreeOfAKind;
            }
            else if (HasTwoPairs(handCards))
            { 
                return WinningHands.TwoPair;
            }
            else if (HasPair(handCards))
            {
                return WinningHands.Pair;
            }
            else
            { 
                return WinningHands.HighCard;
            }            
        }


        public bool HasPair(IEnumerable<Card> _handCards)
        {
            return _handCards.GroupBy(card => card.Face).Count(group => group.Count() == 2) == 1;
        }


        public bool HasTwoPairs(IEnumerable<Card> _handCards)
        {
            return _handCards.GroupBy(card => card.Face).Count(group => group.Count() >= 2) >= 2;
        }

        public bool HasTrips(IEnumerable<Card> _handCards)
        {
            return _handCards.GroupBy(card => card.Face).Count(group => group.Count() == 3) >= 1;
        }

        public bool HasStraight(IEnumerable<Card> _handCards)
        {
            List<Card> orderedCards = _handCards.OrderByDescending(card => card.Face).ToList();
            for (var x = 0; x < orderedCards.Count - 4; x++)
            {
                var skipped = orderedCards.Skip(x);
                var possibleStraight = skipped.Take(5);
                if (IsStraight(possibleStraight.ToList()) || IsHighStraight(possibleStraight.ToList()) || IsLowStraight(possibleStraight.ToList()))
                    return true;
            }
            return false;
        }
        
        public bool IsStraight(List<Card> orderedCards)
        {
            //Counts doubles
            return orderedCards.GroupBy(card => card.Face).Count() == orderedCards.Count() && orderedCards.Max(card => (int)card.Face) - orderedCards.Min(card => (int)card.Face) == 4;
        }

        public bool IsHighStraight(IEnumerable<Card> orderedCards)
        {        
            return orderedCards.Where(card => card.Face == CardFace.Ace && card.Face == CardFace.King && card.Face == CardFace.Queen && card.Face == CardFace.Jack && card.Face == CardFace.Ten).Count() >= 5;
        }

        public bool IsLowStraight(IEnumerable<Card> orderedCards)
        {
            return orderedCards.Where(card => card.Face == CardFace.Ace && card.Face == CardFace.Five && card.Face == CardFace.Four && card.Face == CardFace.Three && card.Face == CardFace.Two).Count() >= 5;
        }

        public bool HasFlush(IEnumerable<Card> _handCards)
        {
            return _handCards.GroupBy(card => card.Suit).Count(group => group.Count() >= 5) == 1;
        }

        public bool HasFullHouse(IEnumerable<Card> _handCards)
        {
            return HasTrips(_handCards) && HasPair(_handCards);
        }

        public bool HasFourOfAKind(IEnumerable<Card> _handCards)
        {
            return _handCards.GroupBy(card => card.Face).Count(group => group.Count() >= 4) == 1;
        }

        public bool HasStraightFlush(IEnumerable<Card> _handCards)
        {
            return HasStraight(_handCards) && HasFlush(_handCards);
        }

        public bool HasRoyalFlush(IEnumerable<Card> _handCards)
        {
            return HasFlush(_handCards) && _handCards.Where(card => card.Face == CardFace.Ace || card.Face == CardFace.King || card.Face == CardFace.Queen || card.Face == CardFace.Jack || card.Face == CardFace.Ten).Count() == 5;
        }
    }
}