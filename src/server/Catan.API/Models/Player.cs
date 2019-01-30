﻿namespace Catan.API.Models
{
    public class Player
    {
        public string Id { get; set; }
        public string Name { get; set; }

        public Player(string id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}
