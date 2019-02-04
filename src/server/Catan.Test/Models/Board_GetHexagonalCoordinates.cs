using Catan.API.Models;
using FluentAssertions;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Catan.Test.Helpers
{
    public class Board_GetHexagonalCoordinates
    {
        private Board _board;

        [Fact]
        public void It_returns_the_coordinates_of_the_hexagon()
        {
            _board = new Board();

            DoTestForHexagonalOfDepth(1, 7);
            DoTestForHexagonalOfDepth(2, 19);
            DoTestForHexagonalOfDepth(3, 37);
        }

        private void DoTestForHexagonalOfDepth(int depth, int expectedCount)
        {
            var coordinates = _board.GetHexagonalCoordinates(depth);
            coordinates.Should().HaveCount(expectedCount);
            CheckCoordinates(coordinates, depth);
        }

        private void CheckCoordinates(List<(int Q, int R, int S)> coordinates, int depth)
        {
            coordinates.Any(co => co.Q < -depth).Should().BeFalse();
            coordinates.Any(co => co.Q > depth).Should().BeFalse();
            coordinates.Any(co => co.R < -depth).Should().BeFalse();
            coordinates.Any(co => co.R > depth).Should().BeFalse();
            coordinates.Any(co => co.S < -depth).Should().BeFalse();
            coordinates.Any(co => co.S > depth).Should().BeFalse();

            coordinates.All(co => co.Q >= -depth && co.Item1 <= depth).Should().BeTrue();
            coordinates.All(co => co.Q >= -depth && co.Item1 <= depth).Should().BeTrue();
            coordinates.All(co => co.R >= -depth && co.Item1 <= depth).Should().BeTrue();
            coordinates.All(co => co.R >= -depth && co.Item1 <= depth).Should().BeTrue();
            coordinates.All(co => co.S >= -depth && co.Item1 <= depth).Should().BeTrue();
            coordinates.All(co => co.S >= -depth && co.Item1 <= depth).Should().BeTrue();

            coordinates.All(DoesSumEqualsZero).Should().BeTrue();
        }

        private bool DoesSumEqualsZero((int Q, int R, int S) co)
        {
            return (co.Q + co.R + co.S) == 0;
        }
    }
}