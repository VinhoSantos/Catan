using Catan.Core.Extensions;
using Catan.Core.Game.Enums;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Catan.Core.Game
{
    [CodeGenerator]
    public class BoardGame
    {
        public Guid Id { get; set; }
        public GameStatus Status { get; set; }
        public Rules Rules { get; set; }
        public Dictionary<string, string> Players { get; set; }
        public GameState GameState { get; set; }

        public BoardGame()
        {
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
                Players = Players.Select(p => new Player(p.Key, p.Value)).ToList()
            };
        }
    }
}