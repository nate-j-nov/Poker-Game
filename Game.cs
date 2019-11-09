using System;
using System.Collections.Generic;
using System.Linq;

namespace PokerGame
{
    public class Game
    {
        public List<Player> Players { get; }
        public Game() {}
        public int RoundCount { get; private set; }
        public LoanShark LoanShark = new LoanShark();
        public HumanPlayer HumanPlayingGame = new HumanPlayer();
        
        

        public Game(IEnumerable<Player> playersInGame)
        {
            Players = playersInGame.ToList();
        }

        public void RunGame()
        {
            do
            {
                if (RoundCount == 0)
                {
                    Console.WriteLine("Good luck!" + Environment.NewLine);
                }
                else
                {
                    Console.WriteLine(Environment.NewLine + "***** Next round! *****" + Environment.NewLine);
                }
                if (HumanPlayingGame.PlayerLoan != null)
                    Console.WriteLine(Environment.NewLine + "Loan Exists" + Environment.NewLine);

                NextRound(2.0, LoanShark, RoundCount);
                Players.RemoveAll(player => player.Money < 0.01);
                DeletePlayersHands();
                RoundCount++;
                if(HumanPlayingGame.PlayerLoan != null)
                    LoanShark.MatureLoan(RoundCount);
            } while (Players.Count() > 1 && !DidHumanDebtExpire() && HumanPlayingGame.Money > 0.01);

            if (DidHumanDebtExpire() || HumanPlayingGame.Money < 0.01)
            {
                Console.WriteLine("Game Over! You lost.");
            }
            else
            {
                GetGameWinner(Players);
            }
        }

        public void GetGameWinner(List<Player> players)
        {
            Console.WriteLine($"{players[0].PlayerName} is the winner!");
        }

        public void NextRound(double v, LoanShark loanShark, int roundNumber)
        {
            var nextRound = new Round(2.0);
            nextRound.RunRound(Players, loanShark, roundNumber);
        }

        public bool DidHumanDebtExpire()
        {
            if (HumanPlayingGame.PlayerLoan != null)
                return HumanPlayingGame.PlayerLoan.DurationOfLoan > 2;// 4;
            else
                return false;
        }
        
        public void DeletePlayersHands()
        {
            foreach(var p in Players)
             {
                p.Hand.Clear();
            }
        }
    }
}
