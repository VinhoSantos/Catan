using Catan.Core.Game.Enums;
using System.Collections.Generic;

namespace Catan.Core.Game
{
    public class GamePlayer
    {
        public Player Player { get; set; }
        public BoardGame Game { get; set; }
        public PlayerColor Color { get; set; }
        public bool CanMakeMove { get; set; }
        public int Points { get; set; }
        public List<ResourceType> ResourceCards { get; set; }
        public List<DevelopmentCard> DevelopmentCards { get; set; }
        public List<Construction> Villages { get; set; }
        public List<Construction> Cities { get; set; }
        public List<Construction> Streets { get; set; }

        public GamePlayer(BoardGame game, Player player, PlayerColor color)
        {
            Game = game;
            Player = player;
            Color = color;
        }
    }

    public class DevelopmentCard
    {
        public DevelopmentCardType Type { get; set; }
        public bool IsActivated { get; set; }
    }
}