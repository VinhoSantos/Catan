using Catan.API.Hubs;
using Catan.Core;
using Catan.Core.Events.Player;
using Catan.Core.Game;
using Microsoft.AspNetCore.SignalR;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Catan.API.Events
{
    public class GameEventsPublisher : IGameEvents
    {
        private readonly IHubContext<ClientHub> _clientHub;

        private IClientProxy Client(string connectionId) => _clientHub.Clients.Client(connectionId);
        private IClientProxy Others(string connectionId) => _clientHub.Clients.AllExcept(connectionId);
        private IClientProxy OthersInGame(string gameId, string exceptConnectionId) => _clientHub.Clients.GroupExcept(gameId, exceptConnectionId);

        public GameEventsPublisher(IHubContext<ClientHub> clientHub)
        {
            _clientHub = clientHub;
        }

        public Task OnPlayerConnected(Player player)
        {
            var tasks = new List<Task>();

            tasks.Add(Client(player.Id).SendAsync("PlayerConnected" , new PlayerConnectedEvent { Id = player.Id, Name = player.Name }));
            tasks.Add(Others(player.Id).SendAsync("OtherPlayerConnected" , new OtherPlayerConnectedEvent { Id = player.Id, Name = player.Name }));

            return Task.WhenAll(tasks);
        }

        public Task OnPlayerDisconnected(Player player)
        {
            var tasks = new List<Task>();

            tasks.Add(Client(player.Id).SendAsync("PlayerDisconnected", new PlayerDisconnectedEvent { Id = player.Id, Name = player.Name }));
            tasks.Add(Others(player.Id).SendAsync("OtherPlayerDisconnected", new OtherPlayerDisconnectedEvent { Id = player.Id, Name = player.Name }));

            return Task.WhenAll(tasks);
        }

        public Task OnGameCreated(BoardGame game)
        {
            throw new System.NotImplementedException();
        }

        public Task OnGameJoined(BoardGame game)
        {
            throw new System.NotImplementedException();
        }

        public Task OnGameStarted(BoardGame game)
        {
            throw new System.NotImplementedException();
        }

        public Task OnGameFinished(BoardGame game)
        {
            throw new System.NotImplementedException();
        }
    }
}
