using PokerGame.Enums;
using System;

namespace PokerGame
{
    public class HumanPlayer : Player
    {
        public bool HasALoan { get; set; }
        int userChoice;
        //public bool InDebt;
        public double DebtOutstanding { get; set; }
        public Loan PlayerLoan = null;
        
        public HumanPlayer(string playerName) : base(playerName)
        {

        }
        public HumanPlayer() { }

        public override Decision PerformTurn()
        {
            Console.WriteLine("What would you like to do?\n" +
               "Press: " + Environment.NewLine +
               "1 for Fold" + Environment.NewLine +
               "2 for Call" + Environment.NewLine +
               "3 for Raise");

            MyBestHand = GetBestHand();
            try
            {
                userChoice = int.Parse(Console.ReadLine()); //Get user input
            }
            catch
            {
                Console.WriteLine("Please input a valid response");
                PerformTurn();
            }

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

        public bool InDebt()
        {
            return PlayerLoan.LoanAmount > 0.01;
        }

        public void AcceptLoan(Loan newLoan)
        {
            PlayerLoan = newLoan;
        }

        public void MakePayment(double payment)
        {
            Money -= payment;
            Console.WriteLine(Environment.NewLine + $"Your current debt is now {PlayerLoan.LoanAmount:c}");
        }

        public void PrintDebtOutstanding()
        {
            Console.WriteLine($"Debt Outstanding: {PlayerLoan.LoanAmount:c}" + Environment.NewLine +
                $"Loan Duration: {PlayerLoan.DurationOfLoan}" + Environment.NewLine);
        }
    }
}

