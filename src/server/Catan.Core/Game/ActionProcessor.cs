using Catan.Core.Helpers;
using System;

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

        public void Dispose()
        {
        }
    }
}
