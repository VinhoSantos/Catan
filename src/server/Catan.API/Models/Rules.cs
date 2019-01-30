using Catan.API.Models.Enums;
using System.Collections.Generic;

namespace Catan.API.Models
{
    public abstract class Rules
    {
        public List<Resource> Resources { get; set; }
        public List<NumberSet> NumberSets { get; set; }
        public List<Port> Ports { get; set; }
        public int MinPlayers { get; set; }
        public int MaxPlayers { get; set; }
        public List<Construction> Constructions { get; set; }
    }

    public class BasicRules : Rules
    {
        public BasicRules()
        {
            MinPlayers = 3;
            MaxPlayers = 4;

            Resources = new List<Resource>
            {
                new Resource
                {
                    ResourceType = ResourceType.Dessert,
                    Amount = 1
                },
                new Resource
                {
                    ResourceType = ResourceType.Wood,
                    Amount = 4
                },
                new Resource
                {
                    ResourceType = ResourceType.Brick,
                    Amount = 3
                },
                new Resource
                {
                    ResourceType = ResourceType.Wool,
                    Amount = 4
                },
                new Resource
                {
                    ResourceType = ResourceType.Wheat,
                    Amount = 4
                },
                new Resource
                {
                    ResourceType = ResourceType.Ore,
                    Amount = 3
                }
            };

            NumberSets = new List<NumberSet>
            {
                new NumberSet
                {
                    Number = 2,
                    Amount = 1
                },
                new NumberSet
                {
                    Number = 3,
                    Amount = 2
                },
                new NumberSet
                {
                    Number = 4,
                    Amount = 2
                },
                new NumberSet
                {
                    Number = 5,
                    Amount = 2
                },
                new NumberSet
                {
                    Number = 6,
                    Amount = 2
                },
                new NumberSet
                {
                    Number = 8,
                    Amount = 2
                },
                new NumberSet
                {
                    Number = 9,
                    Amount = 2
                },
                new NumberSet
                {
                    Number = 10,
                    Amount = 2
                },
                new NumberSet
                {
                    Number = 11,
                    Amount = 2
                },
                new NumberSet
                {
                    Number = 12,
                    Amount = 1
                }
            };

            Ports = new List<Port>
            {
                new Port
                {
                    Type = PortType.ThreeToOne
                },
                new Port
                {
                    Type = PortType.ThreeToOne
                },
                new Port
                {
                    Type = PortType.ThreeToOne
                },
                new Port
                {
                    Type = PortType.ThreeToOne
                },
                new Port
                {
                    Type = PortType.TwoToOneWood
                },
                new Port
                {
                    Type = PortType.TwoToOneBrick
                },
                new Port
                {
                    Type = PortType.TwoToOneWool
                },
                new Port
                {
                    Type = PortType.TwoToOneWheat
                },
                new Port
                {
                    Type = PortType.TwoToOneOre
                }
            };

            Constructions = new List<Construction>
            {
                new Construction
                {
                    Type = ConstructionType.Street,
                    Recources = new List<Resource>
                    {
                        new Resource
                        {
                            ResourceType = ResourceType.Wood,
                            Amount = 1
                        },
                        new Resource
                        {
                            ResourceType = ResourceType.Brick,
                            Amount = 1
                        }
                    }
                },
                new Construction
                {
                    Type = ConstructionType.Village,
                    Recources = new List<Resource>
                    {
                        new Resource
                        {
                            ResourceType = ResourceType.Wood,
                            Amount = 1
                        },
                        new Resource
                        {
                            ResourceType = ResourceType.Brick,
                            Amount = 1
                        },
                        new Resource
                        {
                            ResourceType = ResourceType.Wheat,
                            Amount = 1
                        },
                        new Resource
                        {
                            ResourceType = ResourceType.Wool,
                            Amount = 1
                        }
                    }
                },
                new Construction
                {
                    Type = ConstructionType.Village,
                    Recources = new List<Resource>
                    {
                        new Resource
                        {
                            ResourceType = ResourceType.Wheat,
                            Amount = 2
                        },
                        new Resource
                        {
                            ResourceType = ResourceType.Ore,
                            Amount = 3
                        }
                    }
                },
                new Construction
                {
                    Type = ConstructionType.DevelopmentCard,
                    Recources = new List<Resource>
                    {
                        new Resource
                        {
                            ResourceType = ResourceType.Wheat,
                            Amount = 1
                        },
                        new Resource
                        {
                            ResourceType = ResourceType.Ore,
                            Amount = 1
                        },
                        new Resource
                        {
                            ResourceType = ResourceType.Wool,
                            Amount = 1
                        }
                    }
                }
            };
        }
    }

    public class Construction
    {
        public ConstructionType Type { get; set; }
        public List<Resource> Recources { get; set; }
    }

    public class Port
    {
        public PortType Type { get; set; }
    }

    public class NumberSet
    {
        public int Number { get; set; }
        public int Amount { get; set; }
    }

    public class Resource
    {
        public ResourceType ResourceType { get; set; }
        public int Amount { get; set; }
    }
}
