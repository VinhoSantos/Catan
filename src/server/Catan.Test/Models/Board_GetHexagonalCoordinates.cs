using Catan.API.Models;
using FluentAssertions;
using System;
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

        private void CheckCoordinates(List<Tuple<int, int, int>> coordinates, int depth)
        {
            coordinates.Any(co => co.Item1 < -depth).Should().BeFalse();
            coordinates.Any(co => co.Item1 > depth).Should().BeFalse();
            coordinates.Any(co => co.Item2 < -depth).Should().BeFalse();
            coordinates.Any(co => co.Item2 > depth).Should().BeFalse();
            coordinates.Any(co => co.Item3 < -depth).Should().BeFalse();
            coordinates.Any(co => co.Item3 > depth).Should().BeFalse();

            coordinates.All(co => co.Item1 >= -depth && co.Item1 <= depth).Should().BeTrue();
            coordinates.All(co => co.Item1 >= -depth && co.Item1 <= depth).Should().BeTrue();
            coordinates.All(co => co.Item2 >= -depth && co.Item1 <= depth).Should().BeTrue();
            coordinates.All(co => co.Item2 >= -depth && co.Item1 <= depth).Should().BeTrue();
            coordinates.All(co => co.Item3 >= -depth && co.Item1 <= depth).Should().BeTrue();
            coordinates.All(co => co.Item3 >= -depth && co.Item1 <= depth).Should().BeTrue();

            coordinates.All(DoesSumEqualsZero).Should().BeTrue();
        }

        private bool DoesSumEqualsZero(Tuple<int, int, int> co)
        {
            return (co.Item1 + co.Item2 + co.Item3) == 0;
        }
    }
}