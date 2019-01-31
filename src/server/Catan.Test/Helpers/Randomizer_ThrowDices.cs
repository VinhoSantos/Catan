using Catan.API.Helpers;
using FluentAssertions;
using Xunit;

namespace Catan.Test.Helpers
{
    public class Randomizer_ThrowDices
    {
        [Fact]
        public void It_returns_a_number_between_1_and_12()
        {
            for (var i = 0; i < 1000; i++)
            {
                var eyes = Randomizer.ThrowDices();
                eyes.Should().BeGreaterOrEqualTo(1).And.BeLessOrEqualTo(12);
            }
        }
    }
}