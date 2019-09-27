using System;
using System.Collections.Generic;
using System.Linq;
using PokerGame.Extensions;

namespace PokerGame
{
    public class PlayerList
    {
        //Creates players in the game
        //List of players
        public readonly List<Player> GamePlayers = new List<Player>();

        //Adds above players to a list
        public List<Player> CreatePlayerList(IEnumerable<Player> players)
        {
            foreach(var player in players)
            {
                GamePlayers.Add(player);
            }

            //Test
            foreach(var h in GamePlayers) 
                Console.WriteLine(h.PlayerName);
            return GamePlayers;
        }
    }
}