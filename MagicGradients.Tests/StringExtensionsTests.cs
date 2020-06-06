using FluentAssertions;
using FluentAssertions.Execution;
using Xunit;

namespace MagicGradients.Tests
{
    public class StringExtensionsTests
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
    }
}
