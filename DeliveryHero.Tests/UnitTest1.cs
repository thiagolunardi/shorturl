using System;
using Xunit;

namespace DeliveryHero.Tests
{
    public class UnitTest1
    {
        [Theory]
        [InlineData("()()()()", true)]
        [InlineData("(((((())))))", true)]
        [InlineData("))))", false)]
        [InlineData(")))()", false)]
        [InlineData(")(", false)]
        [InlineData("())(", false)]
        public void Test1(string input, bool expected)
        {
            // Given a string, write an algorithm that will read a string of parentheses from left to right and decide whether the symbols are balanced.
            var handler = new ParentesesHandler();

            var result = handler.Validate(input);


            Assert.Equal(expected, result);

        }
    }
}
