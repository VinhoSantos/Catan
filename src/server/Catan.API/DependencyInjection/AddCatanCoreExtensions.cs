using Catan.API.Events;
using Catan.Core;
using Catan.Core.Game;
using Catan.Core.Game.Players;
using Microsoft.Extensions.DependencyInjection;

namespace Catan.API.DependencyInjection
{
    public static class AddCatanCoreExtensions
    {
        public static void AddCatanCore(this IServiceCollection services)
        {
            services.AddTransient<IPlayerConnector, PlayerConnector>();
            services.AddTransient<IGameEvents, GameEventsPublisher>();
            services.AddSingleton<IPlayerManager, PlayerManager>();
            services.AddSingleton<IGameManager, GameManager>();
        }
    }
}
