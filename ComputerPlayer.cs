using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using System.Linq;
using static PokerGame.Decision;
using static PokerGame.PokerHand;

namespace PokerGame
{
    public class ComputerPlayer : Player
    {
        public ComputerPlayer(string playerName) : base(playerName)
        {

        }
        public override Decision PerformTurn()
        {
            var compChoice = GetDecisionNumber(out double _raiseAmount);
            raiseAmount = _raiseAmount;
            DecisionType compDecicisionType = (DecisionType)compChoice;
            return new Decision(compDecicisionType);
        }

        private int GetDecisionNumber(out double _raiseAmount)
        {
            //double raiseAmount; 
            var myBestHand = Hand.GetBestHand(new List<Card>());
            Random random = new Random();
            var randomDouble = random.NextDouble();
            switch (myBestHand)
            {
                case WinningHands.HighCard:
                    if (Hand.Max(card => card.Face) <= CardFace.Ten)
                    {
                        _raiseAmount = 0;
                        return 1;
                    }
                    else
                    {
                        _raiseAmount = 0;
                        return 2;
                    }

                case WinningHands.Pair:
                    var pairs = Hand
                        .GroupBy(card => card.Face)
                        .Where(group => group.Count() == 2)
                        .SelectMany(group => group.Skip(1))
                        .Max(card => card.Face);
                        
                    if (pairs > CardFace.Ten)
                    {
                        _raiseAmount = .1 * Money;
                        return 3;
                    }
                    else
                    {
                        _raiseAmount = 0;
                        return 2;
                    }

                case WinningHands.TwoPair:
                    _raiseAmount = 0.2 * Money;
                    return 3;

                case WinningHands.ThreeOfAKind:
                    _raiseAmount = 0.3 * Money;
                    return 3;
                

                default:
                    _raiseAmount = 0;
                    return 1;
            }
        }

        public override bool VerifyDecision()
        {
            int choice = 1;
            if (choice == 1)
                return true;
            else
                return false;
        }
    }
}
