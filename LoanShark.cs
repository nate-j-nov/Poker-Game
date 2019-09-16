using System;

namespace PokerGame
{
    public class LoanShark
    {
        public string loanAmt;
        const double interestRate = 0.2;
        public double payment;
        public int time = 0;
        public string response;
        public double debt;

        public LoanShark()
        {
            Console.WriteLine(@"Would you like to take out a loan?" +
                @"Press ""Y"" for Yes or ""N"" for no.");
        }

        public void GetLoan()
        {
            Console.WriteLine("How much woudld you like to borrow?");
            loanAmt = Console.ReadLine();
            if (double.TryParse(loanAmt, out double x)) 
                debt = x;
            else
                Console.WriteLine("Please type a number");
        }

        public void OfferLoan()
        {
            do {
                GetResponse();
                if(VerifyResponse() && response.Equals('y')){
                    AskForAmt();
                }
                    
            } while (!VerifyResponse());
        }

        public void GetResponse()
        {
            response = Console.ReadLine();
            response = response.ToLower();
        }

        public bool VerifyResponse()
        {
            if (response.Length != 1)
            {
                Console.WriteLine(@"Please type ""Y"" for yes or ""N"" for no");
                return false;
            } else if (Convert.ToChar(response) != 'y' || Convert.ToChar(response) != 'n')
            {
                Console.WriteLine(@"Please type ""Y"" for yes or ""N"" for no");
                return false;
            } else
            {
                return true;
            }
        }

        public void AskForAmt()
        {
            System.Console.WriteLine("How much would you like to borrow?");
            loanAmt = Console.ReadLine();
            if(VerifyLoanAmt())
                debt = Double.Parse(loanAmt);
            
        }

        public bool VerifyLoanAmt()
        {
            if(Double.TryParse(loanAmt, out double x))
                return true;
            else{
                Console.WriteLine("Please respond with a valid number.");
                return false;
            }  
        }


        public void MatureLoan()
        {
            debt = interestRate * (1 + interestRate);
            time++;
        }

        public bool InDebt()
        {
            if (debt > 0)
                return true;
            else
                return false;
        }
    }
}