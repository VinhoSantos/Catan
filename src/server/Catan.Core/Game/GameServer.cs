using System.Collections.Concurrent;
using System.Collections.Generic;

namespace Catan.Core.Game
{
    public class GameServer
    {
        private static GameServer _instance;
        private static readonly object PadLock = new object();

        public ConcurrentDictionary<string, Player> ConnectedPlayers { get; set; }
        public List<BoardGame> Games { get; set; }

        public static GameServer Instance
        {
            get
            {
                lock (PadLock)
                {
                    return _instance ?? (_instance = new GameServer());
                }
            }
        }

        public void Initialize()
        {
            ConnectedPlayers = new ConcurrentDictionary<string, Player>();
            Games = new List<BoardGame>();
        }
    }
}
