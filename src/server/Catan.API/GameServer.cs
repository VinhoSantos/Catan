using Catan.API.Helpers;
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
                Board = GenerateBoard(),
                Players = new List<Player>()
            };
        }

        private List<Tile> GenerateBoard()
        {
            var randomResourceTypes = Randomizer.GetRandomListOfResourceTypes(Rules.Resources).ToArray();
            var count = 0;

            return new List<Tile>
            {
                //CORE
                new Tile
                {
                    X = 0, Y = 0, Z = 0, ResourceType = randomResourceTypes[count++]
                },
                //INNER RING
                new Tile
                {
                    X = 1, Y = -1, Z = 0, ResourceType = randomResourceTypes[count++]
                },
                new Tile
                {
                    X = 0, Y = -1, Z = 1, ResourceType = randomResourceTypes[count++]
                },
                new Tile
                {
                    X = -1, Y = 0, Z = 1, ResourceType = randomResourceTypes[count++]
                },
                new Tile
                {
                    X = -1, Y = 1, Z = 0, ResourceType = randomResourceTypes[count++]
                },
                new Tile
                {
                    X = 0, Y = 1, Z = -1, ResourceType = randomResourceTypes[count++]
                },
                new Tile
                {
                    X = 1, Y = 0, Z = -1, ResourceType = randomResourceTypes[count++]
                },
                //OUTER RING
                new Tile
                {
                    X = 2, Y = -2, Z = 0, ResourceType = randomResourceTypes[count++]
                },
                new Tile
                {
                    X = 1, Y = -2, Z = 1, ResourceType = randomResourceTypes[count++]
                },
                new Tile
                {
                    X = 0, Y = -2, Z = +2, ResourceType = randomResourceTypes[count++]
                },
                new Tile
                {
                    X = -1, Y = -1, Z = 2, ResourceType = randomResourceTypes[count++]
                },
                new Tile
                {
                    X = -2, Y = 0, Z = 2, ResourceType = randomResourceTypes[count++]
                },
                new Tile
                {
                    X = -2, Y = 1, Z = 1, ResourceType = randomResourceTypes[count++]
                },
                new Tile
                {
                    X = -2, Y = 2, Z = 0, ResourceType = randomResourceTypes[count++]
                },
                new Tile
                {
                    X = -1, Y = 2, Z = -1, ResourceType = randomResourceTypes[count++]
                },
                new Tile
                {
                    X = 0, Y = 2, Z = -2, ResourceType = randomResourceTypes[count++]
                },
                new Tile
                {
                    X = 1, Y = 1, Z = -2, ResourceType = randomResourceTypes[count++]
                },
                new Tile
                {
                    X = 2, Y = 0, Z = -2, ResourceType = randomResourceTypes[count++]
                },
                new Tile
                {
                    X = 2, Y = -1, Z = -1, ResourceType = randomResourceTypes[count++]
                },
            };
        }
    }
}
