using Catan.API.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Catan.API.Models
{
    public class Board
    {
        private readonly Rules _rules;

        public List<Tile> Tiles { get; set; }

        public Board()
        {
            _rules = new BasicRules();
        }

        public Board(Rules rules)
        {
            _rules = rules;
        }

        private List<Tile> GenerateBoard()
        {
            var randomResourceTypes = Randomizer.GetRandomListOfResourceTypes(_rules.Resources);

            var coordinates = GetHexagonalCoordinates(2);

            var board = new List<Tile>();

            foreach (var coordinate in coordinates)
            {
                board.Add(new Tile
                {
                    X = coordinate.Item1,
                    Y = coordinate.Item2,
                    Z = coordinate.Item3,
                    ResourceType = randomResourceTypes.First()
                });
                randomResourceTypes.RemoveAt(0);
            }

            return board;

            //return new List<Tile>
            //{
            //    //CORE
            //    new Tile
            //    {
            //        X = 0, Y = 0, Z = 0, ResourceType = randomResourceTypes[count++]
            //    },
            //    //INNER RING
            //    new Tile
            //    {
            //        X = 1, Y = -1, Z = 0, ResourceType = randomResourceTypes[count++]
            //    },
            //    new Tile
            //    {
            //        X = 0, Y = -1, Z = 1, ResourceType = randomResourceTypes[count++]
            //    },
            //    new Tile
            //    {
            //        X = -1, Y = 0, Z = 1, ResourceType = randomResourceTypes[count++]
            //    },
            //    new Tile
            //    {
            //        X = -1, Y = 1, Z = 0, ResourceType = randomResourceTypes[count++]
            //    },
            //    new Tile
            //    {
            //        X = 0, Y = 1, Z = -1, ResourceType = randomResourceTypes[count++]
            //    },
            //    new Tile
            //    {
            //        X = 1, Y = 0, Z = -1, ResourceType = randomResourceTypes[count++]
            //    },
            //    //OUTER RING
            //    new Tile
            //    {
            //        X = 2, Y = -2, Z = 0, ResourceType = randomResourceTypes[count++]
            //    },
            //    new Tile
            //    {
            //        X = 1, Y = -2, Z = 1, ResourceType = randomResourceTypes[count++]
            //    },
            //    new Tile
            //    {
            //        X = 0, Y = -2, Z = +2, ResourceType = randomResourceTypes[count++]
            //    },
            //    new Tile
            //    {
            //        X = -1, Y = -1, Z = 2, ResourceType = randomResourceTypes[count++]
            //    },
            //    new Tile
            //    {
            //        X = -2, Y = 0, Z = 2, ResourceType = randomResourceTypes[count++]
            //    },
            //    new Tile
            //    {
            //        X = -2, Y = 1, Z = 1, ResourceType = randomResourceTypes[count++]
            //    },
            //    new Tile
            //    {
            //        X = -2, Y = 2, Z = 0, ResourceType = randomResourceTypes[count++]
            //    },
            //    new Tile
            //    {
            //        X = -1, Y = 2, Z = -1, ResourceType = randomResourceTypes[count++]
            //    },
            //    new Tile
            //    {
            //        X = 0, Y = 2, Z = -2, ResourceType = randomResourceTypes[count++]
            //    },
            //    new Tile
            //    {
            //        X = 1, Y = 1, Z = -2, ResourceType = randomResourceTypes[count++]
            //    },
            //    new Tile
            //    {
            //        X = 2, Y = 0, Z = -2, ResourceType = randomResourceTypes[count++]
            //    },
            //    new Tile
            //    {
            //        X = 2, Y = -1, Z = -1, ResourceType = randomResourceTypes[count++]
            //    },
            //};
        }

        public List<Tuple<int, int, int>> GetHexagonalCoordinates(int depth)
        {
            var coordinates = new List<Tuple<int, int, int>>(); //x, y, z

            for (var q = -depth; q <= depth; q++)
            {
                for (var r = -depth; r <= depth; r++)
                {
                    var x = q;
                    var y = -q - r;
                    var z = r;

                    if (-depth <= y && y <= depth)
                        coordinates.Add(new Tuple<int, int, int>(x, y, z));
                }
            }

            return coordinates;
        }
    }
}
