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

        public WinningHands GetBestHand(IEnumerable<Card> communityCards)
        {
            var handCards = _cards.ToList();
            handCards.AddRange(communityCards);
            
            if (HasTwoPairs(handCards))
            {
                return WinningHands.TwoPair;
            }
            else if (HasTrips(handCards))
            {
                return WinningHands.ThreeOfAKind;
            }
            else if (HasStraight(handCards))
            {
                return WinningHands.Straight;
            }
            else if (HasFlush(handCards))
            {
                if (HasStraightFlush(handCards)
                {
                    return WinningHands.StraightFlush;
                }
                else if (HasRoyalFlush(handCards))
                {
                    return WinningHands.RoyalFlush;
                }
                else
                {
                    return WinningHands.Flush;
                }
            }
            else if (HasFullHouse(handCards))
            {
                return WinningHands.FullHouse;
            }
            else if (HasFourOfAKind(handCards))
            {
                return WinningHands.FourOfAKind;
            }
            else if (HasStraightFlush(handCards))
            {
                return WinningHands.FourOfAKind;
            }
            else if (HasRoyalFlush(handCards))
            {
                return WinningHands.RoyalFlush;
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
            return _handCards.GroupBy(card => card.Face).Count(group => group.Count() == 2) == 2;
        }

        public bool HasTrips(IEnumerable<Card> _handCards)
        {
            return _handCards.GroupBy(card => card.Face).Count(group => group.Count() == 3) == 1;
        }
        //For some reason it's saying it can't convert from IEnumerable 
        public bool HasStraight(IEnumerable<Card> _handCards)
        {
            List<Card> orderedCards = _handCards.OrderByDescending(card => card.Face).ToList();
            for (var x = 0; x < orderedCards.Count - 5; x++)
            {
                var skipped = orderedCards.Skip(x);
                var possibleStraight = skipped.Take(5);
                if (IsStraight(possibleStraight.ToList()) || IsHighStraight(possibleStraight.ToList()) || IsLowStraight(possibleStraight.ToList()))
                    return true;
            }
            return false;
        }
        
        public bool IsStraight(List<Card> _orderedCards)
        {
            //Counts doubles
            bool doubles = _orderedCards.GroupBy(card => card.Face).Count(group => group.Count() > 1) >= 1;
            var inARow = _orderedCards[4].Face - _orderedCards[0].Face == 4;
            return !doubles && inARow;
        }

        public bool IsHighStraight(IEnumerable<Card> _orderedCards)
        {
            //_orderedCards = _orderedCards.ToList();
            return _orderedCards.Where(card => card.Face == CardFace.Ace || card.Face == CardFace.King || card.Face == CardFace.Queen || card.Face == CardFace.Jack || card.Face == CardFace.Ten).Count() >= 5;
        }

        public bool IsLowStraight(IEnumerable<Card> _orderedCards)
        {
            //_orderedCards = _orderedCards.ToList();
            return _orderedCards.Where(card => card.Face == CardFace.Ace || card.Face == CardFace.Two || card.Face == CardFace.Three|| card.Face == CardFace.Four || card.Face == CardFace.Five).Count() >= 5;
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