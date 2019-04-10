namespace Catan.Core.Events.Player
{
    public abstract class PlayerInfo
    {
        public string Id { get; set; }
        public string Name { get; set; }
    }

    public class PlayerConnectedEvent : PlayerInfo
    {
    }

    public class OtherPlayerConnectedEvent : PlayerInfo
    {
    }

    public class PlayerDisconnectedEvent : PlayerInfo
    {
    }

    public class OtherPlayerDisconnectedEvent : PlayerInfo
    {
    }
}
