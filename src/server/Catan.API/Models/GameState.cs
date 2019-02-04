using System.Collections.Generic;

namespace Catan.API.Models
{
    public class GameState
    {
        public List<Player> Players { get; set; }
        public Board Board { get; set; }
    }
}
