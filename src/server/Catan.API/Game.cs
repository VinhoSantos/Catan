using Catan.API.Models;
using System;
using System.Collections.Generic;

namespace Catan.API
{
    public class Game
    {
        public Guid Id { get; set; }
        public GameStatus Status { get; set; }
        public Rules Rules { get; set; }
        public GameState GameState { get; set; }

        public Game()
        {
            Id = Guid.NewGuid();
            Rules = new BasicRules();

            GameState = new GameState
            {
                Board = new Board(Rules),
                Players = new List<Player>()
            };
        }
    }
}