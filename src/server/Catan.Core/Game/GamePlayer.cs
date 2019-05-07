using Catan.Core.Game.Enums;
using Catan.Core.Libs;
using System.Collections.Generic;
using System.Linq;

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
        public List<Construction> Constructions { get; set; }
        public List<Construction> Villages => Constructions.Where(c => c.Type == ConstructionType.Village).ToList();
        public List<Construction> Cities => Constructions.Where(c => c.Type == ConstructionType.City).ToList();
        public List<Construction> Streets => Constructions.Where(c => c.Type == ConstructionType.Street).ToList();

        public GamePlayer(BoardGame game, Player player, PlayerColor color)
        {
            Game = game;
            Player = player;
            Color = color;
        }

        public void AddStreet(List<Hex> coordinates)
        {
            AddConstruction(ConstructionType.Village, coordinates);
        }

        public void AddVillage(List<Hex> coordinates)
        {
            AddConstruction(ConstructionType.Village, coordinates);
        }

        public void AddCity(List<Hex> coordinates)
        {
            AddConstruction(ConstructionType.City, coordinates);
        }

        private void AddConstruction(ConstructionType type, List<Hex> coordinates)
        {
            var construction = new Construction(type, coordinates);
            Constructions.Add(construction);
        }

        public void CalculatePoints()
        {
            
        }
    }

    public class DevelopmentCard
    {
        public DevelopmentCardType Type { get; set; }
        public bool IsPlayed { get; set; }
    }
}