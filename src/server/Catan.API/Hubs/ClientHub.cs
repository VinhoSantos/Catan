using Catan.API.Models;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Catan.API.Hubs
{
    public class ClientHub: Hub
    {
        private GameServer _gameServer;

        public ClientHub()
        {
            _gameServer = GameServer.Instance;
        }

        public override Task OnConnectedAsync()
        {
            var connectionId = Context.ConnectionId;
            var playerCount = _gameServer.ConnectedPlayers.Count;

            var player = new Player(connectionId, $"Speler {playerCount + 1}");

            _gameServer.ConnectedPlayers.TryAdd(connectionId, player);
            playerCount++;

            Console.WriteLine(playerCount);

            Thread.Sleep(1000);

            Clients.Caller.SendAsync("IsConnected", player);
            Clients.Others.SendAsync("PlayerConnected", player);
            
            return Clients.AllExcept(_gameServer.Games.SelectMany(g => g.Players.Keys).ToList()).SendAsync("UpdateAvailableGames", _gameServer.Games);
        }

        public override Task OnDisconnectedAsync(Exception exception)
        {
            var connectionId = Context.ConnectionId;
            _gameServer.ConnectedPlayers.TryRemove(connectionId, out Player player);

            return Clients.All.SendAsync("PlayerDisconnected", player);
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
