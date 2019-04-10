using System.Collections.Generic;

namespace Catan.Core.Game
{
    public class GameState
    {
        public List<Player> Players { get; set; }
        public Board Board { get; set; }
    }
}
