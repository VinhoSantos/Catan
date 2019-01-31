using Catan.API.Helpers;
using Catan.API.Models;
using Catan.API.Models.Enums;
using FluentAssertions;
using System;
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
            var resources = new List<Resource>
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

            var resourceTypes = Randomizer.GetRandomListOfResourceTypes(resources);

            var sumOfAllResourceAmounts = resources.Sum(r => r.Amount);
            var sumOfAllDessert = resources.Where(r => r.ResourceType == ResourceType.Dessert).Sum(r => r.Amount);
            var sumOfAllWood = resources.Where(r => r.ResourceType == ResourceType.Wood).Sum(r => r.Amount);
            var sumOfAllBrick = resources.Where(r => r.ResourceType == ResourceType.Brick).Sum(r => r.Amount);
            var sumOfAllWool = resources.Where(r => r.ResourceType == ResourceType.Wool).Sum(r => r.Amount);
            var sumOfAllWheat = resources.Where(r => r.ResourceType == ResourceType.Wheat).Sum(r => r.Amount);
            var sumOfAllOre = resources.Where(r => r.ResourceType == ResourceType.Ore).Sum(r => r.Amount);

            resourceTypes.Should().HaveCount(sumOfAllResourceAmounts);
            resourceTypes.Where(r => r == ResourceType.Dessert).Should().HaveCount(sumOfAllDessert);
            resourceTypes.Where(r => r == ResourceType.Wood).Should().HaveCount(sumOfAllWood);
            resourceTypes.Where(r => r == ResourceType.Brick).Should().HaveCount(sumOfAllBrick);
            resourceTypes.Where(r => r == ResourceType.Wool).Should().HaveCount(sumOfAllWool);
            resourceTypes.Where(r => r == ResourceType.Wheat).Should().HaveCount(sumOfAllWheat);
            resourceTypes.Where(r => r == ResourceType.Ore).Should().HaveCount(sumOfAllOre);
        }
    }

    public class Randomizer_ThrowDices
    {
        [Fact]
        public void It_returns_a_number_between_1_and_12()
        {
            for (var i = 0; i < 1000; i++)
            {
                var eyes = Randomizer.ThrowDices();
                Console.WriteLine(eyes);
                eyes.Should().BeGreaterOrEqualTo(1).And.BeLessOrEqualTo(12);
            }
        }
    }
}
