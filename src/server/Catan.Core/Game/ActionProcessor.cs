using Catan.Core.Helpers;
using System;
using System.Linq;

namespace Catan.Core.Game
{
    //public interface IActionProcessor : IDisposable
    //{
    //    void ProcessAction(GamePlayer gamePlayer, Action action);
    //}

    public class ActionProcessor : IDisposable
    {
        private readonly BoardGame _game;

        public ActionProcessor(BoardGame game)
        {
            _game = game;
        }
        public void ProcessAction(GamePlayer gamePlayer, Action action)
        {
            switch (action.Type)
            {
                case ActionType.Roll:
                    ProcessRollAction();
                    break;
                case ActionType.Harvest:
                    ProcessHarvestAction();
                    break;
                case ActionType.Buy:
                    break;
                case ActionType.Build:
                    break;
                case ActionType.TradeRequest:
                    break;
                case ActionType.Trade:
                    break;
                case ActionType.Block:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
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

            //todo: check if robber is not on

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

        public void Dispose()
        {
        }
    }
}
