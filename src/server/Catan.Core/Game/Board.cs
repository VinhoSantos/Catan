using Catan.Core.Game.Enums;
using Catan.Core.Helpers;
using Catan.Core.Libs;
using System.Collections.Generic;
using System.Linq;

namespace Catan.Core.Game
{
    public class Board
    {
        private Rules _rules;
        
        public List<Tile> Tiles { get; set; }
        public List<Port> Ports { get; set; }

        public Board()
        {
            _rules = new BasicRules();
            PopulateBoard();
        }

        public Board(Rules rules)
        {
            _rules = rules;
            PopulateBoard();
        }

        private void PopulateBoard()
        {
            PlaceTilesRandomlyOnBoard();
            PlacePortsOnBoard();

        }

        private void PlacePortsOnBoard()
        {
            Ports = new List<Port>();

            foreach (var portRuleSet in _rules.Ports)
            {
                for (var i = 0; i < portRuleSet.Amount; i++)
                {
                    Ports.Add(new Port
                    {
                        Name = portRuleSet.Name,
                        ResourceType = portRuleSet.ResourceType,
                        Hexes = new List<Hex>
                        {
                            new Hex(2, -2, 0),
                            new Hex(2, -1, -1),
                            new Hex(3, -2, -1)
                        }
                    });
                }
            }
        }

        private void PlaceTilesRandomlyOnBoard()
        {
            var randomResourceTypes = Randomizer.GetRandomListOfResourceTypes(_rules.Resources);
            var randomNumbers = GetDefaultListOfNumbers();

            var coordinates = GetHexagonalCoordinates(2);

            Tiles = new List<Tile>();

            foreach (var (x, y, z) in coordinates)
            {
                var resource = randomResourceTypes.First();
                randomResourceTypes.RemoveAt(0);

                int? number = null;
                if (resource != ResourceType.Dessert)
                {
                    number = randomNumbers.First();
                    randomNumbers.RemoveAt(0);
                }

                Tiles.Add(new Tile
                {
                    X = x,
                    Y = y,
                    Z = z,
                    Hex = new Hex(x, y, z),
                    ResourceType = resource,
                    Value = number
                });
            }
        }

        private static List<int> GetDefaultListOfNumbers()
        {
            return new List<int>
            {
                5, 2, 6, 3, 8, 10, 9, 12, 11, 4, 8, 10, 9, 4, 5, 6, 3, 11
            };
        }

        public List<(int X, int Y, int Z)> GetHexagonalCoordinates(int steps)
        {
            var coordinates = new List<(int X, int Y, int Z)>(); //x, y, z

            for (var q = -steps; q <= steps; q++)
            {
                for (var r = -steps; r <= steps; r++)
                {
                    var x = q;
                    var y = -q - r;
                    var z = r;

                    if (-steps <= y && y <= steps)
                        coordinates.Add((x, y, z));
                }
            }

            return coordinates;
        }
    }

    public class Construction
    {
        public ConstructionType Type { get; set; }
        public List<Hex> Coordinates { get; set; }

        public Construction(ConstructionType type, List<Hex> coordinates)
        {
            Type = type;
            Coordinates = coordinates;
        }

        public Construction(ConstructionType type, List<(int x, int y, int z)> coordinates)
        {
            Type = type;
            Coordinates = coordinates.Select(co => new Hex(co.x, co.y, co.z)).ToList();
        }

        public bool IsNeighbourOf(List<Hex> coordinates)
        {
            var equalCount = 0;

            foreach (var coordinate in Coordinates)
            {
                if (coordinates.Contains(coordinate))
                    equalCount++;
            }

            return coordinates.Count - 1 == equalCount;
        }
    }

    public class Port
    {
        public string Name { get; set; }
        public ResourceType? ResourceType { get; set; }
        public List<Hex> Hexes { get; set; }
    }

    public class Tile
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int Z { get; set; }
        public Hex Hex { get; set; }
        public ResourceType ResourceType { get; set; }
        public int? Value { get; set; }
        public bool IsBlocked { get; set; }
    }
}
