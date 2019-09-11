using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using static PokerGame.Dealer;
using static PokerGame.HumanPlayer;
using static PokerGame.Player;
using static PokerGame.Decision;
using static PokerGame.PlayerList;
using static PokerGame.PokerHand;

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
                Console.WriteLine($"{p.PlayerName} has {p.Hand.GetBestHand(CommCards).ToString()}.");
            }

            foreach (var p in playersInRound)
            {
                Console.WriteLine($"{p.PlayerName}'s cards");
                p.PrintHand(CommCards);
                Console.WriteLine("\n");
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
