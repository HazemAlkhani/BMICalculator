using Xunit;
using FluentAssertions;

namespace BMICalculatorApi.Tests
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            int expectedValue = 42;
            int actualValue = 42;
            actualValue.Should().Be(expectedValue, because: "we expect the actual value to match the expected value");
        }
    }
}