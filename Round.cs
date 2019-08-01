using System;
using System.Collections;
using System.Collections.Generic;
using static PokerGame.Dealer;
using static PokerGame.HumanPlayer;
using static PokerGame.Player;
using static PokerGame.Decision;
using static PokerGame.PlayerTest;


namespace PokerGame
{
    public class Round
    {
        public double BetToMatch { get; private set; }
        double RaiseAmount;
        double Pot = 50.00;
        double BigBlind = 2.00;
        double SmallBlind = 1.00;
        //static Decision decision = nate.PerformTurn()
        
        public Round()
        {
            Dealer dealer = new Dealer();
            PlayerTest Players = new PlayerTest();
            List<HumanPlayer> PlayerList = Players.CreatePlayerList();
            
            BetToMatch = BigBlind;            

            dealer.PopulateDeck();
            dealer.ShuffleDeck();

            //Draw community cards
            dealer.DrawFlop();
            dealer.DrawTurn();
            dealer.DrawRiver();

            //Executes turn
            BettingCycle(PlayerList);
            PrintPot();
            //decision.GetType();
        }

        void ExecuteTurn(HumanPlayer player)
        {
            do
            {
                Decision decision = player.PerformTurn();
                switch (decision.SelDecisionType)
                {
                    case DecisionType.Call:
                        Call(player);
                        break;
                    case DecisionType.Fold:
                        Fold(player);
                        break;
                    case DecisionType.Raise:
                        Console.WriteLine("How much you you like to bet?");
                        RaiseAmount = Double.Parse(Console.ReadLine());
                        Raise(player, RaiseAmount);
                        break;
                }
            } while (!player.VerifyDecision());
        }
        //Should Call, Fold, and Raise call a player?

        void Call(HumanPlayer player)
        {
            Pot += BetToMatch;
            player.Money -= BetToMatch;
        }

        //Raises money in the pot
        void Raise(HumanPlayer player, double raiseAmt)
        {
            if (raiseAmt <= player.Money && raiseAmt > 0)
            {
                player.Money -= (raiseAmt + BetToMatch);
                Pot += raiseAmt;
                BetToMatch += RaiseAmount;                
            }
            else
            {
                Console.WriteLine("You can't afford to bet that, " +
                    "but you can borrow from the loan shark if you'd like");
            }
        }

        //Fold function, return true. If Fold = TRUE, round ends for human
        bool Fold(HumanPlayer player)
        {
            return true;
        }

        void PrintPot()
        {
            Console.WriteLine("Pot: {0:c}", Pot);
        }

        bool VerifyResponse(int response)
        {
            if (response >= 1 || response <= 3)
                return true;
            else
                return false;
        }

        void PayBigBlind(HumanPlayer player)
        {
            player.Money -= BigBlind;
            Pot += BigBlind;
            PrintPot();
        }

        void PaySmallBlind(HumanPlayer player)
        {
            player.Money -= SmallBlind;
            Pot += BigBlind;
        }

        void BettingCycle(List<HumanPlayer> playerList)
        {
            foreach (HumanPlayer p in playerList)
            {
                ExecuteTurn(p);
                p.PrintMoney();
                PrintPot();
                Console.ReadLine();
            }
        }
    }
}
