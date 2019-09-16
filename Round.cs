using PokerGame.Enums;
using System;
using System.Collections.Generic;

namespace PokerGame
{
    public class Round //Driver
    {
        private double _ante = 2.00;
        public double BetToMatch { get; private set; }
        double Pot;
        Dealer dealer = new Dealer();
        List<Card> CommCards = new List<Card>();

        public Round(double ante)
        {
            _ante = ante;
        }

        //  I think this and other objects like 'Game' and 'Round' should just be managed and run from the Dealer class.  
        //  Makes sense to me from a modeling standpoint if we are tyring to model the nuances of an actual game - dealer runs everything.
        public void RunRound(List<Player> playersInRound)
        {
            dealer.PopulateDeck();
            dealer.ShuffleDeck();
            PayAntes(playersInRound);
            BetToMatch = _ante;

            foreach(var p in playersInRound)
            {
                dealer.DealPlayerCards(p);
            }

            //Draw community cards
            DrawCards(3);

            //Bet on Community Cards
            

            DrawCards(1);

            DrawCards(1);

            BettingCycle(playersInRound);

            //Populate TotalCards, which is part of my idea to determine the winner of each hand
            
            foreach(var p in playersInRound)
            {
                Console.WriteLine($"{p.PlayerName} has {p.GetBestHand(CommCards).ToString()}.");
            }

            foreach (var p in playersInRound)
            {
                Console.WriteLine($"{p.PlayerName}'s cards");
                p.PrintHand(CommCards);
                Console.WriteLine("\n");
            }
            foreach(var cc in CommCards)
            {
                Console.WriteLine(cc.ToString());
            }

            PrintPot();
            //decision.GetType();
        }

        void ExecuteTurn(Player player)
        {
            do
            {
                Decision decision = player.PerformTurn();
                switch (decision.SelDecisionType)
                {
                    case DecisionType.Call:
                        Call(player);
                        Console.WriteLine($"{player.PlayerName} called.");
                        break;
                    case DecisionType.Fold:
                        Fold(player);
                        Console.WriteLine($"{player.PlayerName} folded.");
                        break;
                    case DecisionType.Raise:
                        Console.WriteLine("How much would you like to bet?");
                        Raise(player);
                        Console.WriteLine($"{player.PlayerName} raised {player.raiseAmount:c}.");
                        break;
                }
            } while (!player.VerifyDecision());
        }

        void Call(Player player)
        {
            Pot += BetToMatch;
            player.Money -= BetToMatch;
        }

        //Raises money in the pot
        void Raise(Player player)
        {
            if (player is HumanPlayer)
            {
                player.raiseAmount = Double.Parse(Console.ReadLine());
            }
            if (player.raiseAmount <= player.Money && player.raiseAmount > 0)
            {
                player.Money -= (player.raiseAmount + BetToMatch);
                Pot += player.raiseAmount;
                BetToMatch = player.raiseAmount;
            }
            else
            {
                Console.WriteLine("You can't afford to bet that, " +
                    "but you can borrow from the loan shark if you'd like");
            }
        }

        //Fold function, return true. If Fold = TRUE, round ends for human
        bool Fold(Player player)
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

        void PayAntes(List<Player> playerList)
        {
            foreach (var p in playerList)
            {
                p.Money -= _ante;
                Pot += _ante;
                Console.WriteLine($"{p.PlayerName} paid their ante");
                PrintPot();
            }
        }

        //Goes through round of betting for each player in PlayerList
        void BettingCycle(List<Player> playerList)
        {
            foreach (Player p in playerList)
            {
                if (p is HumanPlayer)
                {
                    Console.WriteLine("Human Player's Hand:");
                    p.PrintTotalCards(CommCards);
                    Console.WriteLine(Environment.NewLine + "Player " + p.PlayerName + " has a " + p.GetBestHand(CommCards) + Environment.NewLine);
                }
                ExecuteTurn(p);
                p.PrintMoney();
                PrintPot();
                Console.ReadLine();
            }
        }

        public void DrawCards(int numCards)
        {
            for(int i=0 ; i < numCards; i++)
            {
                CommCards.Add(dealer.DrawCard());
            }
            PrintCommCards();
        }

        //Prints Community Cards
        public void PrintCommCards()
        {
            foreach (Card e in CommCards)
                Console.Write("  {0}  ", e.ToString());
            Console.WriteLine();
        }
    }
}
