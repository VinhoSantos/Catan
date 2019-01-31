using Catan.API.Models;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace Catan.API
{
    public class GameServer
    {
        private static GameServer _instance;
        private static readonly object padLock = new object();

        public ConcurrentDictionary<string, Player> ConnectedPlayers { get; set; }
        public GameState GameState { get; set; }
        public Rules Rules { get; set; }

        public static GameServer Instance
        {
            get
            {
                lock (padLock)
                {
                    return _instance ?? (_instance = new GameServer());
                }
            }
        }

        public void Initialize()
        {
            ConnectedPlayers = new ConcurrentDictionary<string, Player>();
            Rules = new BasicRules();

            GameState = new GameState
            {
                Board = new Board(Rules),
                Players = new List<Player>()
            };
        }
    }
}
