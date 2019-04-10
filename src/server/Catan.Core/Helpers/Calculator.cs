namespace Catan.Core.Helpers
{
    public static class Calculator
    {
        public static int AmountOfHexagonalFieldsByDepth(int depth)
        {
            if (depth <= 0)
                return 1;

            var amount = 1;

            for (var i = 1; i <= depth; i++)
            {
                amount += i * 6;
            }

            return amount;
        }

        public static int Factorial(int i)
        {
            if (i <= 1)
                return 1;
            return i * Factorial(i - 1);
        }
    }
}
