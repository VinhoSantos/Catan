using Catan.API.Extensions;
using System.Collections.Generic;

namespace Catan.API.Models
{
    [CodeGenerator]
    public class GameState
    {
        public List<Player> Players { get; set; }
        public Board Board { get; set; }
    }
}
