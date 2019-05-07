using Catan.Core.Game.Enums;
using Catan.Core.Libs;
using System;
using System.Collections.Generic;

namespace Catan.Core.Game
{
    public interface IAction
    {
    }

    public abstract class Action : IAction
    {
        public ActionType Type { get; set; }

        protected Action(ActionType type)
        {
            Type = type;
        }
    }

    public class RollAction : Action
    {
        public RollAction() : base(ActionType.Roll)
        {
        }
    }

    public class HarvestAction : Action
    {
        public ResourceType Resource { get; set; }

        public HarvestAction() : base(ActionType.Harvest)
        {
        }
    }

    public class BuyAction : Action
    {
        public BuyAction() : base(ActionType.Buy)
        {
        }
    }

    public class BuildAction : Action
    {
        public ConstructionType ConstructionType { get; set; }
        public List<Hex> Coordinates { get; set; }

        public BuildAction() : base(ActionType.Build)
        {
        }
    }

    public class TradeRequestAction : Action
    {
        public Guid? PlayerId { get; set; }
        public IEnumerable<ResourceType> ResourcesToGive { get; set; }
        public IEnumerable<ResourceType> ResourcesWanted { get; set; }

        public TradeRequestAction() : base(ActionType.TradeRequest)
        {
        }
    }

    public class TradeAction : Action
    {


        public TradeAction() : base(ActionType.Trade)
        {
        }
    }

    public class BlockAction : Action
    {
        public Hex TileCoordinates { get; set; }

        public BlockAction() : base(ActionType.Block)
        {
        }
    }

    public enum ActionType
    {
        Roll,
        Harvest,
        Buy,
        Build,
        TradeRequest,
        Trade,
        Block
    }
}
