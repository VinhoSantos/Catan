using Catan.Core.Game.Enums;
using System;
using System.Collections.Generic;
using Catan.Core.Extensions;

namespace Catan.Core.Events.Game
{
    [Event]
    public abstract class GameInfo
    {
        public Guid Id { get; set; }
        public GameStatus Status { get; set; }
    }

    public class GameCreatedEvent : GameInfo
    {
    }

    public class GameJoinedEvent : GameInfo
    {
        public IEnumerable<Core.Game.Player> Players { get; set; }
    }

    public class GameStartedEvent : GameInfo
    {
        public IEnumerable<Core.Game.Player> Players { get; set; }
    }

    public class GameFinishedEvent : GameInfo
    {
    }
}
