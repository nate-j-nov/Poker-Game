using System;
using System.Collections.Generic;
using System.Text;
/*using Microsoft.Win32.SafeHandles;
using System.Runtime.InteropServices;
*/
namespace PokerGame
{
    public class Loan
    {
        public double LoanAmount { get; set; }
        private const double _interestRate = 0.5;
        public int LoanStartRoundNumber { get; private set; }
        public int CurrentRoundNumber { get; private set; }
        public int DurationOfLoan = 0;

        public Loan() { }
        public Loan(int roundStart, double loanAmount)
        {
            this.LoanAmount = loanAmount;
            LoanStartRoundNumber = roundStart; 
        }

        private void SetDurationOfLoan(int roundNumber)
        {
            DurationOfLoan++;
        }

        public void DecreaseLoanAmount(double payment)
        {
            LoanAmount -= payment;
        }

        public void IncreaseLoanAmountAfterRound()
        {
            LoanAmount *= (1 + _interestRate);
        }

        public void MatureLoan(int roundNumber)
        {
            SetDurationOfLoan(roundNumber);
            IncreaseLoanAmountAfterRound();
        }
    }
}
