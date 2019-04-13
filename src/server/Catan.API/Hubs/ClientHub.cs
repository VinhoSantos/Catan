using Catan.Core.Game;
using Catan.Core.Game.Players;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Linq;
using System.Threading.Tasks;

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
            var connectionId = Context.ConnectionId;

            _playerConnector.Connect(connectionId);

            return Task.CompletedTask;
        }

        public override Task OnDisconnectedAsync(Exception exception)
        {
            var connectionId = Context.ConnectionId;

            _playerConnector.Disconnect(connectionId);

            return Task.CompletedTask;
        }
        
        public async Task OnCreateGame(string playerId)
        {
            if (_gameServer.Games.Any(g => g.GameState.Players.Any(p => p.Id == playerId)))
                throw new ArgumentException("Player is still active in other game");

            var game = new BoardGame();
            _gameServer.Games.Add(game);

            await Groups.AddToGroupAsync(playerId, game.Id.ToString());

            Clients.Caller.SendAsync("GameCreated", game);
            Clients.GroupExcept(game.Id.ToString(), playerId).SendAsync("PlayerCreatedGame");
        }

        public async Task OnJoinGame(string playerId, Guid gameId)
        {
            if (_gameServer.Games.Any(g => g.GameState.Players.Any(p => p.Id == playerId)))
                throw new ArgumentException("Player is still active in other game");

            var game = _gameServer.Games.Find(g => g.Id == gameId);
            game.Players.Add(playerId, _gameServer.ConnectedPlayers[playerId].Name);

            await Groups.AddToGroupAsync(playerId, gameId.ToString());

            Clients.Caller.SendAsync("GameJoined", game);
            Clients.GroupExcept(game.Id.ToString(), playerId).SendAsync("PlayerJoinedGame");

            if (game.Players.Count == game.Rules.MaxPlayers)
                StartGame(game);
        }

        private async void StartGame(BoardGame boardGame)
        {
            boardGame.Start();
        }
    }
}
