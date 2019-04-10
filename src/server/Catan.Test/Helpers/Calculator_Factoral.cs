using Catan.Core.Helpers;
using FluentAssertions;
using Xunit;

namespace Catan.Test.Helpers
{
    public class Calculator_AmountOfHexagonalFieldsByDepth
    {
        [Fact]
        public void It_returns_the_amount_of_fields_by_depth()
        {
            Calculator.AmountOfHexagonalFieldsByDepth(0).Should().Be(1);
            Calculator.AmountOfHexagonalFieldsByDepth(1).Should().Be(7);
            Calculator.AmountOfHexagonalFieldsByDepth(2).Should().Be(19);
            Calculator.AmountOfHexagonalFieldsByDepth(3).Should().Be(37);
            Calculator.AmountOfHexagonalFieldsByDepth(4).Should().Be(61);
            Calculator.AmountOfHexagonalFieldsByDepth(5).Should().Be(91);
        }
    }

    public class Calculator_Factoral
    {
        [Fact]
        public void It_returns_the_factoral_number()
        {
            Calculator.Factorial(0).Should().Be(1);
            Calculator.Factorial(1).Should().Be(1);
            Calculator.Factorial(2).Should().Be(2);
            Calculator.Factorial(3).Should().Be(6);
            Calculator.Factorial(4).Should().Be(24);
            Calculator.Factorial(5).Should().Be(120);
            Calculator.Factorial(6).Should().Be(720);
            Calculator.Factorial(7).Should().Be(5040);
        }
    }
}