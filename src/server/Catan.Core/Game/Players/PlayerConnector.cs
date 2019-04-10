namespace Catan.Core.Game.Players
{
    public interface IPlayerConnector
    {
        void Connect(string id, string name);
        void Disconnect(string id);
    }

    public class PlayerConnector : IPlayerConnector
    {
        private readonly IGameEvents _gameEvents;
        private readonly IPlayerManager _playerManager;

        public PlayerConnector(IGameEvents gameEvents, IPlayerManager playerManager)
        {
            _gameEvents = gameEvents;
            _playerManager = playerManager;
        }

        public void Connect(string id)
        {
            var player = _playerManager.CreatePlayer(id);
        }

        public void Disconnect(string id)
        {
            throw new System.NotImplementedException();
        }
    }
}
