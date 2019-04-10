namespace Catan.Core.Game
{
    public interface IGameManager
    {
        BoardGame CreateGame(Player player);
        void JoinGame(Player player, BoardGame game);
        void StartGame(BoardGame game);
        void UpdateGame(BoardGame game);
        void FinishGame(BoardGame game);
    }

    public class GameManager : IGameManager
    {
        public BoardGame CreateGame(Player player)
        {
            throw new System.NotImplementedException();
        }

        public void JoinGame(Player player, BoardGame game)
        {
            throw new System.NotImplementedException();
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
    }
}
