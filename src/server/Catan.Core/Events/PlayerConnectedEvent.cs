namespace Catan.Core.Events
{
    public abstract class PlayerConnectedBaseEvent
    {
        public string Id { get; set; }
        public string Name { get; set; }
    }

    public class PlayerConnectedEvent : PlayerConnectedBaseEvent
    {
    }

    public class OtherPlayerConnectedEvent : PlayerConnectedBaseEvent
    {
    }

    public class PlayerDisconnectedEvent : PlayerConnectedBaseEvent
    {
    }

    public class OtherPlayerDisconnectedEvent : PlayerConnectedBaseEvent
    {
    }
}
