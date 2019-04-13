using System;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace Catan.Core.Game
{
    public interface IGameManager
    {
        BoardGame GetGame(Guid id);
        BoardGame CreateGame(Player player);
        void JoinGame(Player player, Guid gameId);
        void StartGame(BoardGame game);
        void UpdateGame(BoardGame game);
        void FinishGame(BoardGame game);
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

        public void JoinGame(Player player, Guid gameId)
        {
            _games.TryGetValue(gameId, out var game);

        }

        public void StartGame(BoardGame game)
        {
            throw new System.NotImplementedException();
        }

        public void UpdateGame(BoardGame game)
        {
            throw new System.NotImplementedException();
        }

        public void FinishGame(BoardGame game)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<BoardGame> GetAllGames()
        {
            return _games.Values;
        }
    }
}
