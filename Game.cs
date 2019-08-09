﻿using System.Collections.Generic;
using System.Linq;

namespace PokerGame
{
    public class Game
    {
        public List<Player> Players { get; }

        public Game(IEnumerable<Player> playersInGame)
        {
            Players = playersInGame.ToList();
        }

        public void NextRound()
        {
            var nextRound = new Round(2.0);
        }
    }
}