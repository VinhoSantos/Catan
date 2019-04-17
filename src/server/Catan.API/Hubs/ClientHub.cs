using Catan.Core.Game;
using Catan.Core.Game.Players;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Threading.Tasks;
using Action = Catan.Core.Game.Action;

namespace Catan.API.Hubs
{
    public class ClientHub: Hub
    {
        private readonly IPlayerConnector _playerConnector;
        private readonly IGameConnector _gameConnector;

        public ClientHub(IPlayerConnector playerConnector, IGameConnector gameConnector)
        {
            _playerConnector = playerConnector;
            _gameConnector = gameConnector;
        }

        public override Task OnConnectedAsync()
        {
            _playerConnector.Connect(Context.ConnectionId);

            return Task.CompletedTask;
        }

        public override Task OnDisconnectedAsync(Exception exception)
        {
            _playerConnector.Disconnect(Context.ConnectionId);

            return Task.CompletedTask;
        }
        
        public Task OnCreateGame(string playerId)
        {
            _gameConnector.CreateGame(Context.ConnectionId);

            return Task.CompletedTask;
        }

        public Task OnJoinGame(string playerId, Guid gameId)
        {
            _gameConnector.JoinGame(playerId, gameId);

            return Task.CompletedTask;
        }

        private Task OnStartGame(Guid gameId)
        {
            _gameConnector.StartGame(gameId);

            return Task.CompletedTask;
        }

        private Task OnDoAction(Guid gameId, Action action)
        {
            _gameConnector.DoAction(Context.ConnectionId, gameId, action);

            return Task.CompletedTask;
        }

        private Task OnRollAction(Guid gameId, RollAction action)
        {
            _gameConnector.DoAction(Context.ConnectionId, gameId, action);

            return Task.CompletedTask;
        }
    }
}
