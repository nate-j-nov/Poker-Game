using System;
using System.Collections;
using System.Text;
using static PokerGame.HumanPlayer;

namespace PokerGame
{
    public class LoanShark
    {
        public double loanAmt;
        const double interestRate = 0.2;
        public double payment;
        public int time = 0;
        public string response;

        public LoanShark()
        {
            Console.WriteLine(@"Would you like to take out a loan?" +
                @"Press ""Y"" for Yes or ""N"" for no.");
        }

        public void GetLoan(double loanAmt)
        {
            this.loanAmt = loanAmt;
        }

        public bool OfferLoan()
        {
            do
            {
                GetResponse();
                VerifyResponse(response);

            } while (!VerifyResponse(response));
        }

        public void GetResponse()
        {
            response = Console.ReadLine();
            response = response.ToLower();
        }

        public bool VerifyResponse(string response)
        {
            if (response.Length != 1)
            {
                Console.WriteLine(@"Please type ""Y"" for yes or ""N"" for no");
                return false;

            } else if (Convert.ToChar(response) != 'y' || Convert.ToChar(response) != 'n')
            {

                return false;

            } else
            {
                return true;
            }
        }

        public void MatureLoan()
        {
            loanAmt = interestRate * (1 + interestRate);
            time++;
        }
    }
}
