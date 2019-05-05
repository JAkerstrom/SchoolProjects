using Nackowski.BusinessLogic;
using Nackowski.Models.Charts;
using System;
using System.Collections.Generic;
using Xunit;

namespace NackowskiTest
{
    public class UnitTest1
    {
        [Fact]
        public void CheckHasBids()
        {
            bool expectedValue = true;

            Calculator calc = new Calculator();

            var actualValue = calc.HasBids(3);

            Assert.Equal(expectedValue, actualValue);
        }

        [Fact]
        public void CheckHighestBid()
        {
            int expectedValue = 8;

            Calculator calc = new Calculator();

            var actualValue = calc.HighestBid(new List<int>() { 1, 2, 3, 8 });

            Assert.Equal(expectedValue, actualValue);
        }

    }
}
