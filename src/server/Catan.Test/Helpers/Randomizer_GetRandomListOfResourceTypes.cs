using Catan.Core.Game;
using Catan.Core.Game.Enums;
using Catan.Core.Helpers;
using FluentAssertions;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Catan.Test.Helpers
{
    public class Randomizer_GetRandomListOfResourceTypes
    {
        [Fact]
        public void It_returns_a_correct_list_of_resourceTypes()
        {
            var resources = new List<ResourceRuleSet>
            {
                new ResourceRuleSet
                {
                    Type = ResourceType.Dessert,
                    Amount = 1
                },
                new ResourceRuleSet
                {
                    Type = ResourceType.Wood,
                    Amount = 4
                },
                new ResourceRuleSet
                {
                    Type = ResourceType.Brick,
                    Amount = 3
                },
                new ResourceRuleSet
                {
                    Type = ResourceType.Wool,
                    Amount = 4
                },
                new ResourceRuleSet
                {
                    Type = ResourceType.Wheat,
                    Amount = 4
                },
                new ResourceRuleSet
                {
                    Type = ResourceType.Ore,
                    Amount = 3
                }
            };

            var resourceTypes = Randomizer.GetRandomListOfResourceTypes(resources);

            var sumOfAllResourceAmounts = resources.Sum(r => r.Amount);
            var sumOfAllDessert = resources.Where(r => r.Type == ResourceType.Dessert).Sum(r => r.Amount);
            var sumOfAllWood = resources.Where(r => r.Type == ResourceType.Wood).Sum(r => r.Amount);
            var sumOfAllBrick = resources.Where(r => r.Type == ResourceType.Brick).Sum(r => r.Amount);
            var sumOfAllWool = resources.Where(r => r.Type == ResourceType.Wool).Sum(r => r.Amount);
            var sumOfAllWheat = resources.Where(r => r.Type == ResourceType.Wheat).Sum(r => r.Amount);
            var sumOfAllOre = resources.Where(r => r.Type == ResourceType.Ore).Sum(r => r.Amount);

            resourceTypes.Should().HaveCount(sumOfAllResourceAmounts);
            resourceTypes.Where(r => r == ResourceType.Dessert).Should().HaveCount(sumOfAllDessert);
            resourceTypes.Where(r => r == ResourceType.Wood).Should().HaveCount(sumOfAllWood);
            resourceTypes.Where(r => r == ResourceType.Brick).Should().HaveCount(sumOfAllBrick);
            resourceTypes.Where(r => r == ResourceType.Wool).Should().HaveCount(sumOfAllWool);
            resourceTypes.Where(r => r == ResourceType.Wheat).Should().HaveCount(sumOfAllWheat);
            resourceTypes.Where(r => r == ResourceType.Ore).Should().HaveCount(sumOfAllOre);
        }
    }
}
