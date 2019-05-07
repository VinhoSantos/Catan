using Catan.Core.Extensions;
using Catan.Core.Game.Enums;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Catan.Core.Game
{
    [Contract]
    public class BoardGame
    {
        private readonly ActionProcessor _actionProcessor;

        public Guid Id { get; set; }
        public GameStatus Status { get; set; }
        public Rules Rules { get; set; }
        public Dictionary<string, Player> Players { get; set; }
        public GameState GameState { get; set; }

        public BoardGame()
        {
            _actionProcessor = new ActionProcessor(this);
            Id = Guid.NewGuid();
            Status = GameStatus.Waiting;
            Rules = new BasicRules();
        }

        public void Start()
        {
            if (Status != GameStatus.Waiting)
                throw new ArgumentException("Can't start game. Game is ongoing.");

            Status = GameStatus.Ongoing;
            GameState = new GameState
            {
                Board = new Board(Rules),
                Players = Players.Select((p, index) => new GamePlayer(this, p.Value, (PlayerColor)index)).ToList(),
                DevelopmentCards = new List<DevelopmentCard>()
            };
        }

        public void End()
        {
            Status = GameStatus.Ended;
        }

        public void AddPlayer(Player player)
        {
            Players.Add(player.Id, player);
        }

        public void RemovePlayer(Player player)
        {
            Players.Remove(player.Id);
        }

        public void DoAction(Player player, Action action)
        {
            var gamePlayer = GameState.Players.SingleOrDefault(p => p.Player == player);
            if (gamePlayer == null)
                return;

            _actionProcessor.ProcessAction(gamePlayer, action);
        }
    }
}