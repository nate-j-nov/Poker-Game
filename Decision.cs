using PokerGame.Enums;

namespace PokerGame
{
    public partial class Decision
    {
        public DecisionType SelDecisionType { get; }
        public double Amount { get; }

        public Decision(DecisionType selDecisionType)
        {
            SelDecisionType = selDecisionType;            
        }
    }
}