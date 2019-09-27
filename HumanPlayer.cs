using PokerGame.Enums;
using System;

namespace PokerGame
{
    public class HumanPlayer : Player
    {
        int userChoice;
        public HumanPlayer(string playerName) : base(playerName)
        {

        }

        public override Decision PerformTurn()
        {
            Console.WriteLine("What would you like to do?\n" +
               "Press: " + Environment.NewLine +
               "1 for Fold" + Environment.NewLine +
               "2 for Call" + Environment.NewLine +
               "3 for Raise");

            MyBestHand = GetBestHand();
            userChoice = int.Parse(Console.ReadLine()); //Get user input
            DecisionType decision = (DecisionType)userChoice;
            PlayersDecision = decision; 
            return new Decision(decision);
        }

        public override bool VerifyDecision()
        {
            if (userChoice >= 1 && userChoice <= 3)
                return true;
            else
                return false;
        }
    }
}

