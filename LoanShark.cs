using System;

namespace PokerGame
{
    public class LoanShark
    {
        private double _loanAmount;
        const double interestRate = 0.2;
        private double _payment;
        public int time = 0;
        public string Response;
        private Loan _loan = new Loan();

        public LoanShark() { }

        public void OfferLoan(int roundNumber, HumanPlayer player)
        {
            Console.WriteLine("Would you like to take out a loan?" + Environment.NewLine + @"Type ""Y"" for yes and ""N"" for no.");
            do
            {
                GetResponse();
                if (Response.Equals("y"))
                {
                    GetLoan(roundNumber, player);
                }
            } while (!VerifyResponse());
        }
        public void GetLoan(int roundNumber, HumanPlayer player)
        {
            Console.WriteLine("How much would you like to borrow?");
            try
            {
                double.TryParse(Console.ReadLine(), out double loanAmount);
                _loanAmount = loanAmount;
                _loan = new Loan(roundNumber, _loanAmount);
                player.DebtOutstanding = loanAmount;
                player.AcceptLoan(_loan);
                Console.WriteLine($"You received a new loan of {_loanAmount:c}.");
            }
            catch
            {
                Console.WriteLine("Please type a valid number");
                GetLoan(roundNumber, player);
            }
        }

        public void GetResponse()
        {
            Response = Console.ReadLine();
            Response = Response.ToLower().Trim();
        }

        public bool VerifyResponse()
        {

            if (Response.Length != 1)
            {
                Console.WriteLine("Wrong Length");
                Console.WriteLine(@"Please type ""Y"" for yes or ""N"" for no");
                return false;
            }
            else if (!(Response.Equals("y") || Response.Equals("n")))
            {
                Console.WriteLine("not y or n");
                Console.WriteLine(Response);
                Console.WriteLine(@"Please type ""Y"" for yes or ""N"" for no");
                return false;
            }
            else
            {
                return true;
            }
        }

        public void AskForRepayment(HumanPlayer humanPlayer)
        {
            Console.WriteLine("Would you like to repay your loan?" + Environment.NewLine + @"Type ""Y"" for yes and ""N"" for no.");
            do
            {
                GetResponse();
            } while (!VerifyResponse());

            if (Response.Equals("y"))
            {
                
                Console.WriteLine("Please enter the amount you'd like to repay.");
                try 
                {
                    double.TryParse(Console.ReadLine(), out double payment);                    
                    _payment = payment;
                    _loan.LoanAmount -= _payment;
                    humanPlayer.MakePayment(_payment);
                }
                catch
                {
                    Console.WriteLine("Please enter a valid number.");
                    AskForRepayment(humanPlayer);
                }
                if (_loan.LoanAmount < 0.01)
                {
                    humanPlayer.PlayerLoan = null;
                    Console.WriteLine("Your loan was paid off.");
                }
                    
            }
        }

        public void MatureLoan(int roundNumber)
        {
            if(_loan != null)
            _loan.MatureLoan(roundNumber);  
        }
    }
}