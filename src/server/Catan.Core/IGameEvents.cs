using Catan.Core.Game;
using System.Threading.Tasks;

namespace Catan.Core
{
    public interface IGameEvents
    {
        Task OnPlayerConnected(Player player);
        Task OnPlayerDisconnected(Player player);
        Task OnGameCreated(BoardGame game, Player player);
        Task OnGameJoined(BoardGame game);
        Task OnGameStarted(BoardGame game);
        Task OnGameEnded(BoardGame game);
    }
}
