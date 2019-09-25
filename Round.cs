using PokerGame.Enums;
using System;
using System.Collections.Generic;
using System.Linq;

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

        //  Makes sense to me from a modeling standpoint if we are tyring to model the nuances of an actual game - dealer runs everything.
        public void RunRound(List<Player> playersInRound)
        {
            dealer.PopulateDeck();
            dealer.ShuffleDeck();
            PayAntes(playersInRound);
            BetToMatch = _ante;
            Player.SetOtherPlayersBets(BetToMatch);
            foreach (var p in playersInRound)
            {
                dealer.DealPlayerCards(p);
            }

            //Draw community cards
            DrawCommunityCards(3);
            //BettingCycle(playersInRound);

            DrawCommunityCards(1);
            //BettingCycle(playersInRound);

            DrawCommunityCards(1);
            BettingCycle(playersInRound);

            Console.WriteLine($"The winner is {GetWinner(playersInRound).PlayerName}");
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
                BetToMatch += player.raiseAmount;
                Player.SetOtherPlayersBets(BetToMatch);
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
            }
        }

        //Goes through round of betting for each player in PlayerList
        void BettingCycle(List<Player> playerList)
        {
            foreach (var p in playerList)
            {
                if (p is HumanPlayer)
                {
                    Console.WriteLine("Human Player's Hand:");
                    p.PrintTotalCards(CommCards);
                    Console.WriteLine(Environment.NewLine + "Player " + p.PlayerName + " has a " + p.GetBestHand() + Environment.NewLine);
                }
                else
                {
                    Console.WriteLine($"Computer Player {p.PlayerName}'s Hand:");
                    p.PrintTotalCards(CommCards);
                }

                ExecuteTurn(p);
                Console.WriteLine(Environment.NewLine + "Other Players' Bets: {0:c}", Player.OtherPlayersBets.ToString());
                p.PrintMoney();
                PrintPot();
                Console.ReadLine();
            }
        }

        public void DrawCommunityCards(int numCards)
        {
            for (int i = 0; i < numCards; i++)
            {
                Card tempCard = dealer.DrawCard();
                CommCards.Add(tempCard);
                Player.PlayerCommCards.Add(tempCard);

            }
            PrintCommCards();
        }

        //Prints Community Card
        public void PrintCommCards()
        {
            foreach (Card e in CommCards)
                Console.Write("  {0}  ", e.ToString());
            Console.WriteLine();
        }
        public Player GetWinner(List<Player> playersInRound)
        {
            List<Player> winners = new List<Player>();
            winners = playersInRound.OrderByDescending(player => player.MyBestHand).GroupBy(player => player.MyBestHand).First(group => group);
        }
    }
}
