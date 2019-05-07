using Catan.Core.Extensions;
using Catan.Core.Game;
using Catan.Core.Game.Enums;
using Catan.Core.Libs;
using System.Collections.Generic;
using System.Linq;

namespace Catan.Core.Validators
{
    public interface IActionValidator
    {
        bool AlreadyIsConstructionAtPosition(List<Hex> coordinates);
        bool IsResourceFieldAlreadyBlocked(Hex tileCoordinates);
        bool ValidateBuildStreet(GamePlayer gamePlayer, BuildAction action);
        bool ValidateBuildVillage(GamePlayer gamePlayer, BuildAction action);
        bool ValidateBuildCity(GamePlayer gamePlayer, BuildAction action);
    }

    public class ActionValidator : IActionValidator
    {
        private readonly BoardGame _game;

        public ActionValidator(BoardGame game)
        {
            _game = game;
        }

        public bool AlreadyIsConstructionAtPosition(List<Hex> coordinates)
        {
            return _game.GameState.Players
                .SelectMany(p => p.Constructions)
                .Any(c => c.Coordinates.IsSameAs(coordinates));
        }

        public bool IsResourceFieldAlreadyBlocked(Hex tileCoordinates)
        {
            var blockedTile = _game.GameState.Board.Tiles.SingleOrDefault(t => t.IsBlocked);
            return blockedTile != null && blockedTile.Hex.Equals(tileCoordinates);
        }

        public bool ValidateBuildStreet(GamePlayer gamePlayer, BuildAction action)
        {
            return HasOwnBuildingsAdjacentToRoad(gamePlayer, action.Coordinates) || HasOwnRoadsAdjacentToRoad(gamePlayer, action.Coordinates);
        }

        public bool ValidateBuildVillage(GamePlayer gamePlayer, BuildAction action)
        {
            return HasAdjacentRoads(action.Coordinates, gamePlayer.Streets) && !HasNeighbours(action.Coordinates);
        }

        public bool ValidateBuildCity(GamePlayer gamePlayer, BuildAction action)
        {
            return gamePlayer.Villages.Any(v => v.Coordinates.IsSameAs(action.Coordinates));
        }

        private bool HasOwnRoadsAdjacentToRoad(GamePlayer gamePlayer, List<Hex> roadCoordinates)
        {
            var adjacentBuildings = GetBuildingsAdjacentToRoad(roadCoordinates);

            foreach (var buildingCoordinates in adjacentBuildings)
            {
                if (!AlreadyIsOtherConstructionAtPosition(gamePlayer, buildingCoordinates) && HasOwnRoadsAdjacentToBuilding(gamePlayer, buildingCoordinates))
                    return true;
            }

            return false;
        }

        private bool HasOwnRoadsAdjacentToBuilding(GamePlayer gamePlayer, List<Hex> buildingCoordinates)
        {
            return AlreadyIsOwnConstructionAtPosition(gamePlayer, buildingCoordinates);
        }

        private bool HasOwnBuildingsAdjacentToRoad(GamePlayer gamePlayer, List<Hex> coordinates)
        {
            var buildings = GetBuildingsAdjacentToRoad(coordinates);

            foreach (var buildingCoordinate in buildings)
            {
                if (AlreadyIsOwnConstructionAtPosition(gamePlayer, buildingCoordinate))
                    return true;
            }

            return false;
        }

        private List<List<Hex>> GetBuildingsAdjacentToRoad(List<Hex> coordinates)
        {
            var commonCoordinates = coordinates.ElementAt(0).Neighbours().Intersect(coordinates.ElementAt(1).Neighbours());

            var buildings = new List<List<Hex>>();
            foreach (var coordinate in commonCoordinates)
            {
                var buildingCoordinates = coordinates.Clone().ToList();
                buildingCoordinates.Add(coordinate);

                buildings.Add(buildingCoordinates);
            }

            return buildings;
        }

        private bool AlreadyIsOwnConstructionAtPosition(GamePlayer gamePlayer, List<Hex> coordinates)
        {
            return gamePlayer.Constructions.Any(c => c.Coordinates.IsSameAs(coordinates));
        }

        private bool AlreadyIsOtherConstructionAtPosition(GamePlayer gamePlayer, List<Hex> coordinates)
        {
            return _game.GameState.Players.Where(p => p != gamePlayer)
                .SelectMany(p => p.Constructions)
                .Any(c => c.Coordinates.IsSameAs(coordinates));
        }

        private bool HasNeighbours(List<Hex> coordinates)
        {
            return _game.GameState.Players
                .SelectMany(p => p.Constructions.Where(c => c.Type != ConstructionType.Street))
                .Any(c => c.IsNeighbourOf(coordinates));
        }

        private bool HasAdjacentRoads(List<Hex> coordinates, List<Construction> roads)
        {
            foreach (var road in roads)
            {
                if (HasAdjacentRoads(coordinates, road))
                    return true;
            }

            return false;
        }

        private bool HasAdjacentRoads(List<Hex> coordinates, Construction road)
        {
            return road.Coordinates.Intersect(coordinates).Count() == road.Coordinates.Count;
        }
    }
}
