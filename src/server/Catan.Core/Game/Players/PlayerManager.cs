using System.Collections.Concurrent;
using System.Collections.Generic;

namespace Catan.Core.Game
{
    public interface IPlayerManager
    {
        Player CreatePlayer(string id);
        Player CreatePlayer(string id, string name);
        Player UpdatePlayer(string id, string name);
        Player GetPlayer(string id);
        IEnumerable<Player> GetAllPlayers();
        Player RemovePlayer(string id);
        Player RemovePlayer(Player player);
    }

    public class PlayerManager : IPlayerManager
    {
        private ConcurrentDictionary<string, Player> _players = new ConcurrentDictionary<string, Player>();
        public Player UpdatePlayer(string id, string name)
        {
            _players.TryGetValue(id, out var player);

            if (player == null)
                return null;

            player.Name = name;
            return player;
        }

        public Player CreatePlayer(string id)
        {
            var player = new Player(id);
            _players.TryAdd(id, player);
            return player;
        }

        public Player CreatePlayer(string id, string name)
        {
            var player = new Player(id, name);
            _players.TryAdd(id, player);
            return player;
        }

        public Player GetPlayer(string id)
        {
            _players.TryGetValue(id, out var player);
            return player;
        }

        public IEnumerable<Player> GetAllPlayers()
        {
            return _players.Values;
        }

        public Player RemovePlayer(string id)
        {
            _players.TryRemove(id, out var deletedPlayer);
            return deletedPlayer;
        }

        public Player RemovePlayer(Player player)
        {
            _players.TryRemove(player.Id, out var deletedPlayer);
            return deletedPlayer;
        }
    }
}
