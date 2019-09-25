using FluentAssertions;
using MagicGradients.Parser.TokenDefinitions;
using System;
using Xunit;

namespace MagicGradients.Tests.Parser
{
    public class LinearGradientDefinitionTests
    {
        [Theory]
        [InlineData("90deg", 270, true)]
        [InlineData("224deg", 44, true)]
        [InlineData("90", 0, false)]
        [InlineData("", 0, false)]
        public void TryConvertDegreeToAngle_CssToken_ConvertedCorrectly(string token, double expectedResult, bool expectedSuccess)
        {
            // Arrange
            var definition = new LinearGradientDefinition();

            // Act
            var success = definition.TryConvertDegreeToAngle(token, out var result);

            // Assert
            success.Should().Be(expectedSuccess);
            result.Should().BeApproximately(expectedResult, 0.00001d);
        }

        [Theory]
        [InlineData("to bottom", 0)]
        [InlineData("to bottom left", 45)]
        [InlineData("to left", 90)]
        [InlineData("to left top", 135)]
        [InlineData("to top", 180)]
        [InlineData("to top right", 225)]
        [InlineData("to right", 270)]
        [InlineData("to right bottom", 315)]
        public void TryConvertNamedDirectionToAngle_ValidToken_ConvertedAngle(string token, double expectedAngle)
        {
            // Arrange
            var definition = new LinearGradientDefinition();

            // Act
            var success = definition.TryConvertNamedDirectionToAngle(token, out var result);

            // Assert
            success.Should().Be(true);
            result.Should().BeApproximately(expectedAngle, 0.00001d);
        }

        [Theory]
        [InlineData("bottom")]
        [InlineData("bottom left")]
        [InlineData("left")]
        [InlineData("left top")]
        [InlineData("top")]
        [InlineData("top right")]
        [InlineData("right")]
        [InlineData("right bottom")]
        public void TryConvertNamedDirectionToAngle_InvalidToken_NoSuccessZeroAngle(string token)
        {
            // Arrange
            var definition = new LinearGradientDefinition();

            // Act
            var success = definition.TryConvertNamedDirectionToAngle(token, out var result);

            // Assert
            success.Should().Be(false);
            result.Should().BeApproximately(0, 0.00001d);
        }

        [Fact]
        public void TryConvertNamedDirectionToAngle_UnknownDirection_ArgumentOutOfRangeException()
        {
            // Arrange
            var definition = new LinearGradientDefinition();

            // Act
            Action action = () => definition.TryConvertNamedDirectionToAngle("to down", out var result);

            // Assert
            action.Should().Throw<ArgumentOutOfRangeException>();
        }
    }
}
