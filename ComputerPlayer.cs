using PokerGame.Enums;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PokerGame
{
    public class ComputerPlayer : Player
    { 
        public ComputerPlayer(string playerName) : base(playerName)
        {

        }
        public Decision PerformTurn(int flips)
        {
            var compChoice = GetDecisionNumber(out double _raiseAmount, flips);
            raiseAmount = _raiseAmount;
            DecisionType compDecicisionType = (DecisionType)compChoice;
            PlayersDecision = compDecicisionType;
            return new Decision(compDecicisionType);
        }

        private int GetDecisionNumber(out double _raiseAmount, int flips)
        {
            MyBestHand = GetBestHand(); 
            Random random = new Random();
            var randomDouble = random.NextDouble();
            var totalHand = new List<Card>();
            totalHand.AddRange(Hand);
            totalHand.AddRange(PlayerCommCards);

            switch (MyBestHand)
            {
               
                case WinningHands.HighCard:
                    if(randomDouble < 0.005)
                    {
                        _raiseAmount = Money;
                        return 3;
                    }

                    if (totalHand.Max(card => card.Face) <= CardFace.Ten || OtherPlayersBets > 0.025 * Money)
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
                    var pairs = totalHand
                        .GroupBy(card => card.Face)
                        .Where(group => group.Count() == 2)
                        .SelectMany(group => group.Skip(1))
                        .Max(card => card.Face);

                    if(randomDouble <= 0.001)
                    {
                        _raiseAmount = Money;
                        return 3;
                    }

                    if (OtherPlayersBets > 0.1 * Money || pairs <= CardFace.Ten)
                    {
                        _raiseAmount = 0;
                        return 1;
                    }
                    else if (OtherPlayersBets >= 0.05 * Money && OtherPlayersBets < 0.1 * Money)
                    {
                        _raiseAmount = 0;
                        return 2;
                    }
                    else
                    {
                        _raiseAmount = (0.11 * Money) - OtherPlayersBets;
                        return 3;
                    }
            
                    
                case WinningHands.TwoPair:
                    if(randomDouble < 0.0025)
                    {
                        _raiseAmount = Money;
                        return 3;
                    }
                   
                    if (OtherPlayersBets > 0.2 * Money)
                    {
                        _raiseAmount = 0;
                        return 1;
                    }
                    else if(OtherPlayersBets < 0.2 * Money && OtherPlayersBets >= 0.15 * Money)
                    {
                        _raiseAmount = 0;
                        return 2;
                    }
                    else
                    {
                        _raiseAmount = (0.21 * Money) - OtherPlayersBets; 
                        return 3;
                    }

                case WinningHands.ThreeOfAKind:
                    if(randomDouble < 0.005)
                    {
                        _raiseAmount = Money;
                        return 3;
                    }

                    if (OtherPlayersBets > 0.3 * Money)
                    {
                        _raiseAmount = 0;
                        return 1;
                    }
                    else if (OtherPlayersBets < 0.3 * Money && OtherPlayersBets >= 0.2 * Money)
                    {
                        _raiseAmount = 0;
                        return 2;
                    }
                    else
                    {
                        _raiseAmount = (0.31 * Money) - OtherPlayersBets;
                        return 3;
                    }
                    

                case WinningHands.Straight:
                    if(randomDouble < 0.005)
                    {
                        _raiseAmount = Money;
                        return 3;
                    }

                    if (OtherPlayersBets > 0.32 * Money)
                    {
                        _raiseAmount = 0;
                        return 1;
                    }
                    else if(OtherPlayersBets < 0.32 && OtherPlayersBets > 0.27)
                    {
                        _raiseAmount = 0;
                        return 2;
                    }
                    else
                    {
                        _raiseAmount = (0.33 * Money) - OtherPlayersBets;
                        return 3;
                    }

                case WinningHands.Flush:
                    if (randomDouble < 0.0075)
                    {
                        _raiseAmount = Money;
                        return 3;
                    }

                    if(OtherPlayersBets > 0.43 * Money)
                    {
                        _raiseAmount = 0;
                        return 1;
                    }
                    else if (OtherPlayersBets < 0.43 * Money && OtherPlayersBets > 0.38)
                    {
                        _raiseAmount = 0;
                        return 2;
                    }
                    else
                    {
                        _raiseAmount = (0.44 * Money) - OtherPlayersBets;
                        return 3;
                    }
                    

                case WinningHands.FullHouse:
                    if(randomDouble < 0.008)
                    {
                        _raiseAmount = Money;
                        return 3;
                    }

                    if(OtherPlayersBets > 0.5 * Money)
                    {
                        _raiseAmount = 0;
                        return 1;
                    }
                    else if (OtherPlayersBets < 0.5 * Money && OtherPlayersBets > 0.45 * Money)
                    {
                        _raiseAmount = 0;
                        return 2;
                    }
                    else
                    {
                        _raiseAmount = (0.51 * Money) - OtherPlayersBets;
                        return 3;
                    }

                case WinningHands.FourOfAKind:
                    if(randomDouble < 0.05)
                    {
                        _raiseAmount = Money;
                        return 3;
                    }

                    if(OtherPlayersBets > 0.60 * Money)
                    {
                        _raiseAmount = 0;
                        return 1;
                    }
                    else if(OtherPlayersBets < 0.60 * Money && OtherPlayersBets > 0.55 * Money)
                    {
                        _raiseAmount = 0;
                        return 2;
                    }
                    else
                    {
                        _raiseAmount = (0.61 * Money) - OtherPlayersBets;
                        return 3;
                    }

                case WinningHands.StraightFlush:
                    if(randomDouble < 0.5)
                    {
                        _raiseAmount = Money;
                        return 3;
                    }

                    if (OtherPlayersBets > 0.9 * Money)
                    {
                        _raiseAmount = 0;
                        return 2;
                    }
                    else
                    {
                        _raiseAmount = (0.91 * Money) - OtherPlayersBets;
                        return 3;
                    }

                case WinningHands.RoyalFlush:
                    _raiseAmount = Money;
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

        public override Decision PerformTurn()
        {
            throw new NotImplementedException();
        }
    }
}
