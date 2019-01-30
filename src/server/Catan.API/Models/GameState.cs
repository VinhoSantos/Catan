using Catan.API.Models.Enums;
using System.Collections.Generic;

namespace Catan.API.Models
{
    public class GameState
    {
        public List<Player> Players { get; set; }
        public List<Tile> Board { get; set; }
    }

    public class Tile
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int Z { get; set; }
        public ResourceType ResourceType { get; set; }
    }
}
