using System.Collections.Generic;

namespace Catan.Core.Game
{
    public class GameState
    {
        public List<GamePlayer> Players { get; set; }
        public Board Board { get; set; }
        public List<DevelopmentCard> DevelopmentCards { get; set; }
        public int LastDiceRoll { get; set; }
    }
}
