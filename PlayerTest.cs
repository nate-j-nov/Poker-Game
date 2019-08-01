using System;
using System.Collections.Generic;
using System.Linq;
using static PokerGame.Player;
using static PokerGame.HumanPlayer;
namespace PokerGame
{
    public class PlayerTest
    {

        public HumanPlayer nate = new HumanPlayer("Nate");
        public HumanPlayer jake = new HumanPlayer("Jake");
        public HumanPlayer evan = new HumanPlayer("Evan");
        public HumanPlayer chad = new HumanPlayer("Chad");

        public List<HumanPlayer> HumanPlayers = new List<HumanPlayer>();

        public List<HumanPlayer> CreatePlayerList()
        {
            HumanPlayers.Add(nate);
            HumanPlayers.Add(jake);
            HumanPlayers.Add(evan);
            HumanPlayers.Add(chad);

            
            foreach(var h in HumanPlayers)
                Console.WriteLine(h.PlayerName);
            return HumanPlayers;
        }

        public PlayerTest() { }
    }
}
