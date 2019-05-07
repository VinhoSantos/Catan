using Catan.Core.Game.Enums;
using Catan.Core.Helpers;
using Catan.Core.Validators;
using System;
using System.Linq;

namespace Catan.Core.Game
{
    public class ActionProcessor : IDisposable
    {
        private readonly BoardGame _game;
        private readonly IActionValidator _validator;

        public ActionProcessor(BoardGame game)
        {
            _game = game;
            _validator = new ActionValidator(game);
        }
        public void ProcessAction(GamePlayer gamePlayer, Action action)
        {
            if (!gamePlayer.CanMakeMove)
                return;

            switch (action.Type)
            {
                case ActionType.Roll:
                    ProcessRollAction();
                    break;
                case ActionType.Harvest:
                    ProcessHarvestAction();
                    break;
                case ActionType.Buy:
                    ProcessBuyAction(gamePlayer);
                    break;
                case ActionType.Build:
                    ProcessBuildAction(gamePlayer, action as BuildAction);
                    break;
                case ActionType.TradeRequest:
                    ProcessTradeRequestAction(gamePlayer, action as TradeRequestAction);
                    break;
                case ActionType.Trade:
                    ProcessTradeAction(gamePlayer, action as TradeAction);
                    break;
                case ActionType.Block:
                    ProcessBlockAction(action as BlockAction);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void ProcessBlockAction(BlockAction action)
        {
            if (_validator.IsResourceFieldAlreadyBlocked(action.TileCoordinates))
                throw new Exception("Robber has to be moved to a different place");

            _game.GameState.Board.Tiles.Single(t => t.Hex.Equals(action.TileCoordinates)).IsBlocked = true;
        }

        private void ProcessRollAction()
        {
            _game.GameState.LastDiceRoll = Randomizer.ThrowDices();
        }

        private void ProcessHarvestAction()
        {
            var tiles = _game.GameState.Board.Tiles.Where(t =>
                t.Value.HasValue 
                && t.Value.GetValueOrDefault() == _game.GameState.LastDiceRoll
                && !t.IsBlocked);
            
            foreach (var tile in tiles)
            {
                foreach (var gamePlayer in _game.GameState.Players)
                {
                    var villages = gamePlayer.Villages.Where(v => v.Coordinates.Contains(tile.Hex));
                    var cities = gamePlayer.Villages.Where(v => v.Coordinates.Contains(tile.Hex));

                    foreach (var village in villages)
                    {
                        gamePlayer.ResourceCards.Add(tile.ResourceType);
                    }

                    foreach (var city in cities)
                    {
                        gamePlayer.ResourceCards.Add(tile.ResourceType);
                        gamePlayer.ResourceCards.Add(tile.ResourceType);
                    }
                }
            }
        }

        private void ProcessBuyAction(GamePlayer gamePlayer)
        {
            var developmentCard = Randomizer.PopRandomDevelopmentCardFromList(_game.GameState.DevelopmentCards);
            
            foreach (var resourceRuleSet in _game.Rules.DevelopmentCardRuleSet.Cost)
            {
                var amountOfResources = gamePlayer.ResourceCards.Count(r => r == resourceRuleSet.Type);
                if (amountOfResources < resourceRuleSet.Amount)
                    return;

                for (var i = 0; i < amountOfResources; i++)
                {
                    gamePlayer.ResourceCards.Remove(resourceRuleSet.Type);
                }
            }

            gamePlayer.DevelopmentCards.Add(developmentCard);
        }

        private void ProcessBuildAction(GamePlayer gamePlayer, BuildAction action)
        {
            var constructionRuleSet = _game.Rules.Constructions.Single(c => c.Type == action.ConstructionType);

            /*todo: validations
             check if player has still constructions from that specific type left
             check if it is possible to place construction at that position: 
             - nothing already built on that position
             - are there streets nearby
             - villages/cities can't be too close
             */

            if (!gamePlayer.Constructions.Any(c => c.Type == action.ConstructionType && c.Coordinates.Count == 0))
                return;

            if (_validator.AlreadyIsConstructionAtPosition(action.Coordinates))
                return;

            switch (action.ConstructionType)
            {
                case ConstructionType.Street:
                    if (!_validator.ValidateBuildStreet(gamePlayer, action))
                        return;
                    break;
                case ConstructionType.Village:
                    if (!_validator.ValidateBuildVillage(gamePlayer, action))
                        return;
                    break;
                case ConstructionType.City:
                    if (_validator.ValidateBuildCity(gamePlayer, action))
                        return;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            foreach (var resourceRuleSet in constructionRuleSet.Cost)
            {
                var amountOfResources = gamePlayer.ResourceCards.Count(r => r == resourceRuleSet.Type);
                if (amountOfResources < resourceRuleSet.Amount)
                    return;

                for (var i = 0; i < amountOfResources; i++)
                {
                    gamePlayer.ResourceCards.Remove(resourceRuleSet.Type);
                }
            }
        }

        private void ProcessTradeRequestAction(GamePlayer gamePlayer, TradeRequestAction action)
        {
            //todo
        }

        private void ProcessTradeAction(GamePlayer gamePlayer, TradeAction action)
        {
            //todo
        }

        public void Dispose()
        {
        }
    }
}
