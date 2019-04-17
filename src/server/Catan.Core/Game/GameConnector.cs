using System;

namespace Catan.Core.Game
{
    public interface IGameConnector
    {
        void CreateGame(string connectionId);
        void JoinGame(string connectionId, Guid gameId);
        void StartGame(Guid gameId);
        //void UpdateGame();
        void EndGame(Guid gameId);
        void LeaveGame(string connectionId, Guid gameId);
        void DoAction(string connectionId, Guid gameId, Action action);
    }

    public class GameConnector : IGameConnector
    {
        private readonly IGameEvents _gameEvents;
        private readonly IGameManager _gameManager;
        private readonly IPlayerManager _playerManager;

        public GameConnector(IGameEvents gameEvents, IGameManager gameManager, IPlayerManager playerManager)
        {
            _gameEvents = gameEvents;
            _gameManager = gameManager;
            _playerManager = playerManager;
        }

        public void CreateGame(string connectionId)
        {
            var player = _playerManager.GetPlayer(connectionId);
            var game = _gameManager.CreateGame(player);

            _gameEvents.OnGameCreated(game, player);
        }

        public void JoinGame(string connectionId, Guid gameId)
        {
            var player = _playerManager.GetPlayer(connectionId);
            var game = _gameManager.JoinGame(player, gameId);

            _gameEvents.OnGameJoined(game);
        }

        public void StartGame(Guid gameId)
        {
            var game = _gameManager.StartGame(gameId);

            _gameEvents.OnGameStarted(game);
        }

        public void EndGame(Guid gameId)
        {
            var game = _gameManager.EndGame(gameId);

            _gameEvents.OnGameEnded(game);
        }

        public void LeaveGame(string connectionId, Guid gameId)
        {
            var player = _playerManager.GetPlayer(connectionId);
            _gameManager.LeaveGame(player, gameId);
        }

        public void DoAction(string connectionId, Guid gameId, Action action)
        {
            var player = _playerManager.GetPlayer(connectionId);
            _gameManager.DoAction(player, gameId, action);
        }
    }
}
