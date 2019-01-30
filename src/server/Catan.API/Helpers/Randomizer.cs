using Catan.API.Models;
using Catan.API.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Catan.API.Helpers
{
    public static class Randomizer
    {
        public static List<ResourceType> GetRandomListOfResourceTypes(List<Resource> resources)
        {
            var resourceTypes = new List<ResourceType>();
            var resourcesTypesToChooseFrom = new List<ResourceType>();

            foreach (var resource in resources)
            {
                for (var i = 0; i < resource.Amount; i++)
                {
                    resourcesTypesToChooseFrom.Add(resource.ResourceType);
                }
            }

            while (resourcesTypesToChooseFrom.Count > 0)
            {
                var randomIndex = new Random().Next(resourcesTypesToChooseFrom.Count);

                resourceTypes.Add(resourcesTypesToChooseFrom.ElementAt(randomIndex));
                resourcesTypesToChooseFrom.RemoveAt(randomIndex);
            }

            return resourceTypes;
        }

        public static int ThrowDice()
        {
            var random = new Random();
            var dice1 = random.Next(7);
            var dice2 = random.Next(7);

            return dice1 + dice2;
        }
    }
}
