using Catan.API.Publishers;
using Catan.Core;
using Catan.Core.Game;
using Catan.Core.Game.Players;
using Catan.Core.Validators;
using Microsoft.Extensions.DependencyInjection;

namespace Catan.API.DependencyInjection
{
    public static class AddCatanCoreExtensions
    {
        public static void AddCatanCore(this IServiceCollection services)
        {
            services.AddTransient<IPlayerConnector, PlayerConnector>();
            services.AddTransient<IGameConnector, GameConnector>();
            services.AddTransient<IGameEvents, GameEventsPublisher>();
            services.AddTransient<IActionValidator, ActionValidator>();
            services.AddSingleton<IPlayerManager, PlayerManager>();
            services.AddSingleton<IGameManager, GameManager>();
        }
    }
}
