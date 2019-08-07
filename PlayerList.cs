using System;
using System.Collections.Generic;
using System.Linq;
using static PokerGame.Player;
using static PokerGame.HumanPlayer;
namespace PokerGame
{
    public class PlayerList
    {
        //Creates players in the game
        public HumanPlayer nate = new HumanPlayer("Nate");
        public ComputerPlayer jake = new ComputerPlayer("Jake");
        public ComputerPlayer evan = new ComputerPlayer("Evan");
        public ComputerPlayer chad = new ComputerPlayer("Chad");

        //List of players
        public List<Player> GamePlayers = new List<Player>();

        //Adds above players to a list
        public List<Player> CreatePlayerList()
        {
            GamePlayers.Add(nate);
            GamePlayers.Add(jake);
            GamePlayers.Add(evan);
            GamePlayers.Add(chad);

            //Test
            foreach(var h in GamePlayers) 
                Console.WriteLine(h.PlayerName);
            return GamePlayers;
        }

        public PlayerList() { }
    }
}
