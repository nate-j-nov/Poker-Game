using System;
namespace PokerGame
{
    public class Decision
    {
        public DecisionType SelDecisionType { get; }
        public double Amount { get; }

        public enum DecisionType
        {
            Fold = 1,
            Raise,
            Call,
        }

        public Decision(DecisionType selDecisionType, double amnt)
        {
            SelDecisionType = selDecisionType;
            Amount = amnt;
        }
    }
}   


