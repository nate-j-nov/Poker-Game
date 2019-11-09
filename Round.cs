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
        private List<Player> _roundWinners { get; set; }
        public Round() { }
        public LoanShark loanShark = new LoanShark();
        public int RoundCount { get; set; }
        public Round(double ante)
        {
            _ante = ante;
        }

        //  Makes sense to me from a modeling standpoint if we are tyring to model the nuances of an actual game - dealer runs everything.
        public void RunRound(List<Player> participants, LoanShark ls, int roundNumber)
        {
            loanShark = ls;
            List<Player> playersInRound = new List<Player>();
            playersInRound.AddRange(participants);
            dealer.PopulateDeck();
            dealer.ShuffleDeck();
            PayAntes(playersInRound);
            BetToMatch = _ante;
            Player.SetOtherPlayersBets(BetToMatch);
            foreach (var p in playersInRound)
            {
                dealer.DealPlayerCards(p);
            }

            int flips = 0;
            do
            {
                int cardsToDraw;
                if (flips == 0)
                {
                    cardsToDraw = 3;
                    DrawCommunityCards(cardsToDraw);
                }
                else
                {
                    cardsToDraw = 1;
                    DrawCommunityCards(cardsToDraw);
                }
                BettingCycle(playersInRound, flips);
                playersInRound.RemoveAll(player => player.PlayersDecision == DecisionType.Fold);
                flips++;
            } while (flips < 3 && playersInRound.Count() > 1);

            foreach(var p in playersInRound)
            {
                Console.WriteLine($"{p.PlayerName}'s Hand: ");
                p.PrintPlayerHand();
                Console.WriteLine(Environment.NewLine + $"{p.PlayerName}'s Winning Hand: {p.MyBestHand.ToString()}" + Environment.NewLine); 
            }
            _roundWinners = GetWinner(playersInRound);

            DistributeWinnings(_roundWinners);
            DeleteHands(playersInRound);
            DeletePlayerCommCards();
            CommCards.Clear();
        }
        void ExecuteTurn(Player player, int roundNumber)
        {
            Decision decision = new Decision();
            do
            {
                if(player is ComputerPlayer)
                {
                    ComputerPlayer tempComputerPlayer = (ComputerPlayer)player;
                    decision = tempComputerPlayer.PerformTurn(roundNumber);
                }
                else
                {
                    decision = player.PerformTurn();   
                }
                
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
                        if(player is HumanPlayer)
                        {
                            HumanPlayer humanPlayer = (HumanPlayer)player;
                            
                        }
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
                try 
                {
                    player.raiseAmount = Double.Parse(Console.ReadLine());
                }
                catch
                {
                    Console.WriteLine("Please enter a valid numerical value for your bet." + 
                        Environment.NewLine + "How much would you like to bet?");
                    Raise(player);
                }
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
                if(p.Money < _ante)
                {
                    Pot += p.Money;
                    p.Money = 0;
                }
                else
                {
                    p.Money -= _ante;
                    Pot += _ante;
                    Console.WriteLine($"{p.PlayerName} paid their ante");
                }
                
            }
            Console.WriteLine();
        }

        //Goes through round of betting for each player in PlayerList
        void BettingCycle(List<Player> playerList, int roundNumber)
        {
            foreach (var p in playerList)
            {
                if (p is HumanPlayer)
                {
                    Console.WriteLine(Environment.NewLine + "Human Player's Hand:");
                    p.PrintTotalCards(CommCards);
                    Console.WriteLine(Environment.NewLine + "Player " + p.PlayerName + " has a " + p.GetBestHand() + Environment.NewLine);
                    HumanPlayer hp = new HumanPlayer();
                    hp = (HumanPlayer)p;
                    if (hp.PlayerLoan != null)
                    {
                        hp.PrintDebtOutstanding();
                        loanShark.AskForRepayment(hp);
                    }
                    else
                    {
                        loanShark.OfferLoan(roundNumber, hp);
                    }
                }

                ExecuteTurn(p, roundNumber);
                Console.WriteLine(Environment.NewLine + "Bet you have to match: {0:c}", Player.OtherPlayersBets);
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
        public List<Player> GetWinner(List<Player> playersInRound)
        { 
            List<Player> roundWinners = new List<Player>();

            if (playersInRound.Where(player => player.MyBestHand == playersInRound.Max(x => x.MyBestHand)).Count() == 1)
            {
                roundWinners.Add(playersInRound.OrderByDescending(player => player.MyBestHand).First());
                return roundWinners;
            }
            else
            {
                if(playersInRound.Where(player => player.MyBestHand == playersInRound.Max(x => x.MyBestHand) && player.BestWinningFace == playersInRound.Max(x => x.BestWinningFace)).Count() == 1) 
                { 
                    roundWinners.Add(playersInRound.OrderByDescending(player => player.MyBestHand).ThenByDescending(x => x.BestWinningFace).First()); 
                } 
                else
                {
                    roundWinners.AddRange(playersInRound.Where(player => player.MyBestHand == playersInRound.Max(x => x.MyBestHand) && player.BestWinningFace == playersInRound.Max(x => x.BestWinningFace)));
                }
                
                return roundWinners;
            }
        }

        public void DistributeWinnings(List<Player> winners)
        {
            if (winners.Count() == 0)
            {
                Console.WriteLine("There is no winner.");
            } 
            else if (winners.Count == 1)
            {
                Console.WriteLine($"{winners.First().PlayerName} is the winner.");
            }
            else
            {
                Console.WriteLine("The winners are: ");
                foreach (var p in winners)
                {
                    Console.WriteLine($"{p.PlayerName}");
                }
                Console.WriteLine(Environment.NewLine);
            }
            double eachPlayersWinnings = Pot / winners.Count();

            foreach(var p in winners)
            {
                p.Money += eachPlayersWinnings;
                Console.WriteLine("Player won: {0:c}", eachPlayersWinnings);
                Console.WriteLine($"{p.PlayerName}'s money: {p.Money:c}");
            }
        }

        public void DeleteHands(List<Player> playerList)
        {
            foreach (var p in playerList)
                p.Hand.Clear();
        }
        public void DeletePlayerCommCards()
        {
            Player.PlayerCommCards.Clear();
        }
    }
}
