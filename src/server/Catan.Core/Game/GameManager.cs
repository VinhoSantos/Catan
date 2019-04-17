using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace Catan.Core.Game
{
    public interface IGameManager
    {
        BoardGame GetGame(Guid id);
        BoardGame CreateGame(Player player);
        BoardGame JoinGame(Player player, Guid gameId);
        BoardGame LeaveGame(Player player, Guid gameId);
        BoardGame StartGame(Guid gameId);
        BoardGame UpdateGame(BoardGame game);
        BoardGame EndGame(Guid gameId);
        void DoAction(Player player, Guid gameId, Action action);
        IEnumerable<BoardGame> GetAllGames();
    }

    public class GameManager : IGameManager
    {
        private ConcurrentDictionary<Guid, BoardGame> _games = new ConcurrentDictionary<Guid, BoardGame>();

        public BoardGame GetGame(Guid id)
        {
            _games.TryGetValue(id, out var game);

            return game;
        }

        public BoardGame CreateGame(Player player)
        {
            var game = new BoardGame();
            _games.TryAdd(game.Id, game);

            return game;
        }

        public BoardGame JoinGame(Player player, Guid gameId)
        {
            _games.TryGetValue(gameId, out var game);
            game?.AddPlayer(player);

            return game;
        }

        public BoardGame LeaveGame(Player player, Guid gameId)
        {
            _games.TryGetValue(gameId, out var game);
            game?.RemovePlayer(player);

            return game;
        }

        public BoardGame StartGame(Guid gameId)
        {
            _games.TryGetValue(gameId, out var game);
            game?.Start();

            return game;
        }

        public BoardGame UpdateGame(BoardGame game)
        {
            throw new System.NotImplementedException();
        }

        public BoardGame EndGame(Guid gameId)
        {
            _games.TryGetValue(gameId, out var game);
            game?.End();

            return game;
        }

        public void DoAction(Player player, Guid gameId, Action action)
        {
            _games.TryGetValue(gameId, out var game);
            game.DoAction(player, action);
        }

        public IEnumerable<BoardGame> GetAllGames()
        {
            return _games.Values;
        }

        private bool IsPlayerInGame(Player player)
        {
            return GetAllGames().Any(g => g.Players.Any(p => p.Key == player.Id));
        }
    }
}
