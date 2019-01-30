using Catan.API.Models;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Threading.Tasks;

namespace Catan.API.Hubs
{
    public class ClientHub: Hub
    {
        public override Task OnConnectedAsync()
        {
            var connectionId = Context.ConnectionId;
            var playerCount = GameServer.Instance.ConnectedPlayers.Count;

            var player = new Player(connectionId, $"Speler {playerCount + 1}");

            GameServer.Instance.ConnectedPlayers.TryAdd(connectionId, player);
            GameServer.Instance.GameState.Players.Add(player);
            playerCount++;

            Console.WriteLine(playerCount);

            Clients.Caller.SendAsync("PlayerConnected", player);
            return Clients.Others.SendAsync("NewPlayerConnected", player);
        }

        public override Task OnDisconnectedAsync(Exception exception)
        {
            var connectionId = Context.ConnectionId;
            GameServer.Instance.ConnectedPlayers.TryRemove(connectionId, out Player player);

            return Clients.All.SendAsync("PlayerDisconnected", player);
        }
    }
}
