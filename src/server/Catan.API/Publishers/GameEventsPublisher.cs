using Catan.API.Hubs;
using Catan.Core;
using Catan.Core.Events.Game;
using Catan.Core.Events.Player;
using Catan.Core.Game;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Catan.API.Publishers
{
    public class GameEventsPublisher : IGameEvents
    {
        private readonly IHubContext<ClientHub> _clientHub;

        private IClientProxy Client(string connectionId) => _clientHub.Clients.Client(connectionId);
        private IClientProxy Others(string connectionId) => _clientHub.Clients.AllExcept(connectionId);
        private IClientProxy All() => _clientHub.Clients.All;
        private IClientProxy OthersInGame(Guid gameId, string exceptConnectionId) => _clientHub.Clients.GroupExcept(gameId.ToString(), exceptConnectionId);
        private IClientProxy AllInGame(Guid gameId) => _clientHub.Clients.Group(gameId.ToString());

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

        public Task OnGameCreated(BoardGame game, Player player)
        {
            

            var tasks = new List<Task>();

            tasks.Add(Client(player.Id).SendAsync("GameCreated", new GameCreatedEvent { Id = game.Id, Status = game.Status }));
            tasks.Add(All().SendAsync("GameCreated", new GameCreatedEvent { Id = game.Id, Status = game.Status}));
            
            return Task.WhenAll(tasks);
        }

        public Task OnGameJoined(BoardGame game)
        {
            var tasks = new List<Task>();

            tasks.Add(AllInGame(game.Id).SendAsync("GameJoined", new GameJoinedEvent { Id = game.Id, Status = game.Status, Players = game.Players.Select(p => new Player(p.Key, p.Value.Name)) }));

            return Task.WhenAll(tasks);
        }

        public Task OnGameStarted(BoardGame game)
        {
            return AllInGame(game.Id).SendAsync("GameStarted", new GameStartedEvent { Id = game.Id, Status = game.Status, Players = game.Players.Select(p => new Player(p.Key, p.Value.Name)) });
        }

        public Task OnGameEnded(BoardGame game)
        {
            return AllInGame(game.Id).SendAsync("GameFinished", new GameFinishedEvent { Id = game.Id, Status = game.Status });
        }
    }
}
