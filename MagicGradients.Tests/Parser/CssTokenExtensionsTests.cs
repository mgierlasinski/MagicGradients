using FluentAssertions;
using FluentAssertions.Execution;
using MagicGradients.Parser;
using Xunit;

namespace MagicGradients.Tests.Parser
{
    public class CssTokenExtensionsTests
    {
        [Theory]
        [InlineData("15%", "%", 15)]
        [InlineData("20px", "px", 20)]
        [InlineData("4.5ss", "ss", 4.5)]
        public void TryExtractNumber_ValidValue_ExtractedNumber(string input, string unit, float expected)
        {
            // Act
            var success = input.TryExtractNumber(unit, out var result);

            // Assert
            using (new AssertionScope())
            {
                success.Should().Be(true);
                result.Should().Be(expected);
            }
        }

        [Theory]
        [InlineData("70%", true, 0.7)]
        [InlineData("20px", true, 20)]
        [InlineData("15ss", false, 0)]
        public void TryConvertOffset_GivenValue_ProperStatusAndNumber(string input, bool expectedSuccess, float expectedValue)
        {
            // Act
            var success = input.TryConvertOffset(out var result);

            // Assert
            using (new AssertionScope())
            {
                success.Should().Be(expectedSuccess);
                result.Should().Be(expectedValue);
            }
        }
    }
}
