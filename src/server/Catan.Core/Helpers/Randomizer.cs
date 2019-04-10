using Catan.Core.Game;
using Catan.Core.Game.Enums;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Catan.Core.Helpers
{
    public static class Randomizer
    {
        public static List<ResourceType> GetRandomListOfResourceTypes(List<ResourceRuleSet> resources)
        {
            var resourceTypes = new List<ResourceType>();
            var resourcesTypesToChooseFrom = new List<ResourceType>();

            foreach (var resource in resources)
            {
                for (var i = 0; i < resource.Amount; i++)
                {
                    resourcesTypesToChooseFrom.Add(resource.Type);
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

        public static List<int> GetRandomListOfNumbers(List<NumberRuleSet> numberRuleSet)
        {
            var numbers = new List<int>();
            var numbersToChooseFrom = new List<int>();

            foreach (var numberSet in numberRuleSet)
            {
                for (var i = 0; i < numberSet.Amount; i++)
                {
                    numbersToChooseFrom.Add(numberSet.Number);
                }
            }

            while (numbersToChooseFrom.Count > 0)
            {
                var randomIndex = new Random().Next(numbersToChooseFrom.Count);

                numbers.Add(numbersToChooseFrom.ElementAt(randomIndex));
                numbersToChooseFrom.RemoveAt(randomIndex);
            }

            return numbers;
        }

        public static int ThrowDices()
        {
            var random = new Random();
            var dice1 = random.Next(1, 7);
            var dice2 = random.Next(1, 7);

            return dice1 + dice2;
        }
    }
}
