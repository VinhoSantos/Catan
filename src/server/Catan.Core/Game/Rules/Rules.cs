using Catan.Core.Game.Enums;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Catan.Core.Game
{
    public abstract class Rules
    {
        public int MinPlayers { get; set; }
        public int MaxPlayers { get; set; }
        public int PointsToWin { get; set; }
        public List<ResourceRuleSet> Resources { get; set; }
        public List<NumberRuleSet> NumberSets { get; set; }
        public List<PortRuleSet> Ports { get; set; }
        public List<ConstructionRuleSet> Constructions { get; set; }
        public List<DevelopmentCardRuleSet> DevelopmentCards { get; set; }
        public List<EffectCardRuleSet> EffectCards { get; set; }
    }

    public class EffectCardRuleSet
    {
        public string Name { get; set; }
        public int ExtraPoints { get; set; }
    }

    public class BasicRules : Rules
    {
        public BasicRules()
        {
            MinPlayers = 3;
            MaxPlayers = 4;
            PointsToWin = 10;

            Resources = new List<ResourceRuleSet>
            {
                new ResourceRuleSet
                {
                    Type = ResourceType.Dessert,
                    Color = "#c2b280",
                    Amount = 1
                },
                new ResourceRuleSet
                {
                    Type = ResourceType.Wood,
                    Color = "#228B22",
                    Amount = 4
                },
                new ResourceRuleSet
                {
                    Type = ResourceType.Brick,
                    Color = "#dc5539",
                    Amount = 3
                },
                new ResourceRuleSet
                {
                    Type = ResourceType.Wool,
                    Color = "#e1e0d8",
                    Amount = 4
                },
                new ResourceRuleSet
                {
                    Type = ResourceType.Wheat,
                    Color = "#f5deb3",
                    Amount = 4
                },
                new ResourceRuleSet
                {
                    Type = ResourceType.Ore,
                    Color = "#999999",
                    Amount = 3
                }
            };

            NumberSets = new List<NumberRuleSet>
            {
                new NumberRuleSet
                {
                    Number = 2,
                    Amount = 1
                },
                new NumberRuleSet
                {
                    Number = 3,
                    Amount = 2
                },
                new NumberRuleSet
                {
                    Number = 4,
                    Amount = 2
                },
                new NumberRuleSet
                {
                    Number = 5,
                    Amount = 2
                },
                new NumberRuleSet
                {
                    Number = 6,
                    Amount = 2
                },
                new NumberRuleSet
                {
                    Number = 8,
                    Amount = 2
                },
                new NumberRuleSet
                {
                    Number = 9,
                    Amount = 2
                },
                new NumberRuleSet
                {
                    Number = 10,
                    Amount = 2
                },
                new NumberRuleSet
                {
                    Number = 11,
                    Amount = 2
                },
                new NumberRuleSet
                {
                    Number = 12,
                    Amount = 1
                }
            };

            Ports = new List<PortRuleSet>
            {
                new PortRuleSet
                {
                    Name = "3/1",
                    Pay = 3,
                    Receive = 1,
                    Amount = 4
                },
                new PortRuleSet
                {
                    Name = "2/1 Wood",
                    Pay = 2,
                    Receive = 1,
                    Amount = 2
                },
                new PortRuleSet
                {
                    Name = "2/1 Steen",
                    Pay = 2,
                    Receive = 1,
                    Amount = 2
                },
                new PortRuleSet
                {
                    Name = "2/1 Wool",
                    Pay = 2,
                    Receive = 1,
                    Amount = 2
                },
                new PortRuleSet
                {
                    Name = "2/1 Wheat",
                    Pay = 2,
                    Receive = 1,
                    Amount = 2
                },
                new PortRuleSet
                {
                    Name = "2/1 Ore",
                    Pay = 2,
                    Receive = 1,
                    Amount = 2
                }
            };

            Constructions = new List<ConstructionRuleSet>
            {
                new ConstructionRuleSet
                {
                    Type = ConstructionType.Street,
                    Cost = new List<ResourceRuleSet>
                    {
                        new ResourceRuleSet
                        {
                            Type = ResourceType.Wood,
                            Amount = 1
                        },
                        new ResourceRuleSet
                        {
                            Type = ResourceType.Brick,
                            Amount = 1
                        }
                    }
                },
                new ConstructionRuleSet
                {
                    Type = ConstructionType.Village,
                    Cost = new List<ResourceRuleSet>
                    {
                        new ResourceRuleSet
                        {
                            Type = ResourceType.Wood,
                            Amount = 1
                        },
                        new ResourceRuleSet
                        {
                            Type = ResourceType.Brick,
                            Amount = 1
                        },
                        new ResourceRuleSet
                        {
                            Type = ResourceType.Wheat,
                            Amount = 1
                        },
                        new ResourceRuleSet
                        {
                            Type = ResourceType.Wool,
                            Amount = 1
                        }
                    }
                },
                new ConstructionRuleSet
                {
                    Type = ConstructionType.Village,
                    Cost = new List<ResourceRuleSet>
                    {
                        new ResourceRuleSet
                        {
                            Type = ResourceType.Wheat,
                            Amount = 2
                        },
                        new ResourceRuleSet
                        {
                            Type = ResourceType.Ore,
                            Amount = 3
                        }
                    }
                }
            };

            DevelopmentCards = new List<DevelopmentCardRuleSet>
            {
                new DevelopmentCardRuleSet
                {
                    Cost = new List<ResourceRuleSet>
                    {
                        new ResourceRuleSet
                        {
                            Type = ResourceType.Wool,
                            Amount = 1
                        },
                        new ResourceRuleSet
                        {
                            Type = ResourceType.Wheat,
                            Amount = 1
                        },
                        new ResourceRuleSet
                        {
                            Type = ResourceType.Ore,
                            Amount = 1
                        }
                    },
                    DevelopmentCards = new List<DevelopmentCardTypeRuleSet>
                    {
                        new DevelopmentCardTypeRuleSet
                        {
                            Type = DevelopmentCardType.Knight,
                            Amount = 14
                        },
                        new DevelopmentCardTypeRuleSet
                        {
                            Type = DevelopmentCardType.VictoryPoint,
                            Amount = 5
                        },
                        new DevelopmentCardTypeRuleSet
                        {
                            Type = DevelopmentCardType.Streets,
                            Amount = 2
                        },
                        new DevelopmentCardTypeRuleSet
                        {
                            Type = DevelopmentCardType.Resources,
                            Amount = 2
                        },
                        new DevelopmentCardTypeRuleSet
                        {
                            Type = DevelopmentCardType.Monopoly,
                            Amount = 2
                        }
                    }
                }
            };

            Validate();
        }

        private void Validate()
        {
            if (Resources.Where(r => r.Type != ResourceType.Dessert).Sum(r => r.Amount) != NumberSets.Sum(n => n.Amount))
                throw new ArgumentException("Rules invalid: The amount of resources must match the amount of numbers");
        }
    }

    public class ConstructionRuleSet
    {
        public ConstructionType Type { get; set; }
        public List<ResourceRuleSet> Cost { get; set; }
    }

    public class PortRuleSet
    {
        public string Name { get; set; }
        public ResourceType? ResourceType { get; set; }
        public int Receive { get; set; }
        public int Pay { get; set; }
        public int Amount { get; set; }
    }

    public class NumberRuleSet
    {
        public int Number { get; set; }
        public int Amount { get; set; }
    }

    public class ResourceRuleSet
    {
        public ResourceType Type { get; set; }
        public string Color { get; set; }
        public int Amount { get; set; }
    }

    public class DevelopmentCardRuleSet
    {
        public List<ResourceRuleSet> Cost { get; set; }
        public List<DevelopmentCardTypeRuleSet> DevelopmentCards { get; set; }
    }

    public class DevelopmentCardTypeRuleSet
    {
        public DevelopmentCardType Type { get; set; }
        public int Amount { get; set; }
    }
}
