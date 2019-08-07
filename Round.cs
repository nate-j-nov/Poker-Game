using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using static PokerGame.Dealer;
using static PokerGame.HumanPlayer;
using static PokerGame.Player;
using static PokerGame.Decision;
using static PokerGame.PlayerList;


namespace PokerGame
{
    public class Round //Driver
    {
        public double BetToMatch { get; private set; }
        double RaiseAmount;
        double Pot = 50.00;
        double Ante = 2.00;
        
        Dealer dealer = new Dealer();
        List<Card> CommCards = new List<Card>();

        public Round()
        {
            PlayerList Players = new PlayerList();
            List<Player> PlayersInGame = Players.CreatePlayerList();

            dealer.PopulateDeck();
            dealer.ShuffleDeck();

            PayAntes(PlayersInGame);

            PlayersInGame.ForEach(dealer.DealPlayerCards);
         
            //Draw community cards
            DrawFlop();

            //Bet on Community Cards
            BettingCycle(PlayersInGame);

            /*dealer.DrawTurn();
            BettingCycle(PlayerList);

            dealer.DrawRiver();
            BettingCycle(PlayerList);*/

            //Populate TotalCards, which is part of my idea to determine the winner of each hand
            foreach (var p in PlayersInGame)
            {
                p.PopulateTotalCards(CommCards);
            }

            //Print both a players hand and the community card. This is a test.
            foreach(var p in PlayersInGame)
            {
                Console.WriteLine($"{p.PlayerName}'s cards");
                p.PrintTotalCards();
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
                        Console.WriteLine("How much you you like to bet?");
                        RaiseAmount = Double.Parse(Console.ReadLine());
                        Raise(player, RaiseAmount);
                        Console.WriteLine($"{player.PlayerName} raised {RaiseAmount:c}.");
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
        void Raise(Player player, double raiseAmt)
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

        /*void PayBigBlind(Player player)
        {
            
        }*/

        /*void PaySmallBlind(Player player)
        {
            player.Money -= SmallBlind;
            Pot += BigBlind;
        }*/

        //Method for each player to pay ante prior to betting
        void PayAntes(List<Player> playerList)
        {
            foreach (var p in playerList)
            {
                p.Money -= Ante;
                Pot += Ante;
                Console.WriteLine($"{p.PlayerName} paid their ante");
                PrintPot();
            }
        }

        //Goes through round of betting for each player in PlayerList
        void BettingCycle(List<Player> playerList)
        {
            foreach (Player p in playerList)
            {
                ExecuteTurn(p);
                p.PrintMoney();
                PrintPot();
                Console.ReadLine();
            }
        }

        //Draws first three cards of Community Cards
        public void DrawFlop()
        {
            for (int i = 0; i < 3; i++)
            {
                CommCards.Add(dealer.DrawCard());
            }
            PrintCommCards();
        }

        //Draws fourth community card
        public void DrawTurn()
        {
            if (CommCards.Count == 3)
            {
                CommCards.Add(dealer.DrawCard());
                PrintCommCards();
            }
            else
            {
                Console.WriteLine("Can't do this");
            }
        }

        //Draws fifth community card
        public void DrawRiver()
        {
            if (CommCards.Count == 4)
            {
                CommCards.Add(dealer.DrawCard());
                PrintCommCards();
            }
            else
            {
                Console.WriteLine("Can't do this");
            }
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
