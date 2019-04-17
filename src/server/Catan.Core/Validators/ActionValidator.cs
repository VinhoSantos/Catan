using Catan.Core.Game;
using Action = System.Action;

namespace Catan.Core.Validators
{
    public interface IActionValidator
    {
        bool IsValid(Action action, BoardGame game, GamePlayer gamePlayer);
    }

    public class ActionValidator : IActionValidator
    {
        public bool IsValid(Action action, BoardGame game, GamePlayer gamePlayer)
        {
            return true;
        }
    }
}
