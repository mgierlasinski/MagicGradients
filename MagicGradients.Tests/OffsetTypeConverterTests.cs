using FluentAssertions;
using FluentAssertions.Execution;
using Xunit;

namespace MagicGradients.Tests
{
    public class OffsetTypeConverterTests
    {
        [Theory]
        [InlineData("70%", true, 0.7)]
        [InlineData("20px", true, 20)]
        [InlineData("15ss", false, 0)]
        public void TryConvertOffset_GivenValue_ProperStatusAndNumber(string input, bool expectedSuccess, double expectedValue)
        {
            // Arrange
            var converter = new OffsetTypeConverter();

            // Act
            var success = converter.TryExtractOffset(input, out var result);

            // Assert
            using (new AssertionScope())
            {
                success.Should().Be(expectedSuccess);
                result.Value.Should().Be(expectedValue);
            }
        }

        [Theory]
        [InlineData("%", 0, false)]
        [InlineData("", 0, false)]
        [InlineData(null, 0, false)]
        [InlineData("10", 0, false)]
        [InlineData("0%", 0, true)]
        [InlineData("23%", 0.23, true)]
        [InlineData("101%", 1, true)]
        [InlineData("100%", 1, true)]
        public void TryConvertPercentToOffset_ConvertingToken_SuccessAndResultConvertedCorrectly(string token, double expectedResult, bool expectedSuccess)
        {
            // Arrange
            var converter = new OffsetTypeConverter();

            // Act
            var success = converter.TryExtractOffset(token, out var result);

            // Assert
            success.Should().Be(expectedSuccess);
            result.Value.Should().Be(expectedResult);
        }
    }
}
