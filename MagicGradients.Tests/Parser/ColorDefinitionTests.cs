using FluentAssertions;
using MagicGradients.Parser;
using MagicGradients.Parser.TokenDefinitions;
using System;
using Xunit;

namespace MagicGradients.Tests.Parser
{
    public class ColorDefinitionTests
    {
        [Theory]
        [InlineData("rgb(4, 164, 188)", "rgb(4,164,188)")]
        [InlineData("rgba(4, 164, 188, 0.1)", "rgba(4,164,188,0.1)")]
        [InlineData("hsl(4, 10%, 35%)", "hsl(4,10%,35%)")]
        [InlineData("hsla(4, 10%, 35%,0.55)", "hsla(4,10%,35%,0.55)")]
        [InlineData("rgb(100%, 12%, 43%)", "rgb(100%,12%,43%)")]
        [InlineData("rgba(100%, 12%, 43%,0.17)", "rgba(100%,12%,43%,0.17)")]
        public void GetColorString_ReadFromCssReader_CorrectColorStringReturned(string colorInCss, string expectedColorInString)
        {
            // Arrange
            var definition = new ColorDefinition();
            var reader = new CssReader(colorInCss);

            // Act
            var colorInString = definition.GetColorString(reader);

            // Assert
            colorInString.Should().Be(expectedColorInString);
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
        public void TryConvertPercentToOffset_ConvertingToken_SuccessAndResultConvertedCorrectly(string token, float expectedResult, bool expectedSuccess)
        {
            // Arrange
            var definition = new ColorDefinition();

            // Act
            var success = definition.TryConvertPercentToOffset(token, out var result);

            // Assert
            success.Should().Be(expectedSuccess);
            result.Should().Be(expectedResult);
        }
    }
}
