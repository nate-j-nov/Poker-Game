using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using System.Linq;
using static PokerGame.Decision;

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
               "Press: 1 for Fold \n" +
               "2 for Raise \n" +
               "3 for Call");

            userChoice = int.Parse(Console.ReadLine());
            DecisionType decision = (DecisionType)userChoice;
            return new Decision(decision, 10.24);
        }

        public bool VerifyDecision()
        {
            if (userChoice >= 1 && userChoice <= 3)
                return true;
            else
                return false;
            
        }
    }
}

