using FluentAssertions;
using FluentAssertions.Execution;
using MagicGradients.Parser;
using MagicGradients.Parser.TokenDefinitions;
using System;
using MagicGradients.Tests.Mock;
using Xamarin.Forms;
using Xunit;

namespace MagicGradients.Tests.Parser.TokenDefinitions
{
    public class LinearGradientDefinitionTests
    {
        public LinearGradientDefinitionTests()
        {
            Device.PlatformServices = new MockPlatformServices();
        }

        [Theory]
        [InlineData(CssToken.LinearGradient, true)]
        [InlineData(CssToken.RepeatingLinearGradient, true)]
        [InlineData(CssToken.Hsla, false)]
        [InlineData(CssToken.Rgba, false)]
        public void IsMatch_ProvidedToken_CorrectMatchResult(string token, bool expectedMatch)
        {
            // Arrange
            var definition = new LinearGradientDefinition();

            // Act
            var isMatch = definition.IsMatch(token);

            // Assert
            isMatch.Should().Be(expectedMatch);
        }

        [Theory]
        [InlineData("90deg", 270, true)]
        [InlineData("224deg", 44, true)]
        [InlineData("0deg", 180, true)]
        [InlineData("0", 180, true)]
        [InlineData("90", 0, false)]
        [InlineData("", 0, false)]
        public void TryConvertDegreeToAngle_CssToken_ConvertedCorrectly(string token, double expectedResult, bool expectedSuccess)
        {
            // Arrange
            var definition = new LinearGradientDefinition();

            // Act
            var success = definition.TryConvertDegreeToAngle(token, out var result);

            // Assert
            using (new AssertionScope())
            {
                success.Should().Be(expectedSuccess);
                result.Should().BeApproximately(expectedResult, 0.00001d);
            }
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
            using (new AssertionScope())
            {
                success.Should().Be(true);
                result.Should().BeApproximately(expectedAngle, 0.00001d);
            }
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
            using (new AssertionScope())
            {
                success.Should().Be(false);
                result.Should().BeApproximately(0, 0.00001d);
            }
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

        [Theory]
        [ClassData(typeof(LinearGradientDefinitionData))]
        public void Parse_ValidGradientCss_ExpectedGradientReturned(string css, LinearGradient expectedGradient)
        {
            // Arrange
            var reader = new CssReader(css);
            var builder = new GradientBuilder();
            var definition = new LinearGradientDefinition();

            // Act
            definition.Parse(reader, builder);

            // Assert
            var result = builder.Build();

            using (new AssertionScope())
            {
                result.Should().HaveCount(1);
                var gradient = result[0] as LinearGradient;
                gradient.Should().NotBeNull();
                gradient.Should().BeEquivalentTo(expectedGradient);
            }
        }
    }
}
