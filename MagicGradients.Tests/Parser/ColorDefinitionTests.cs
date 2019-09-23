using System;
using System.Collections.Generic;
using System.Text;
using FluentAssertions;
using MagicGradients.Parser;
using MagicGradients.Parser.TokenDefinitions;
using Xunit;

namespace MagicGradients.Tests.Parser
{
    public class ColorDefinitionTests
    {
        [Theory]
        [InlineData("rgb(4, 164, 188)", "rgb(4,164,188)")]
        [InlineData("rgba(4, 164, 188, 0.1)", "rgba(4,164,188,0.1)")]
        [InlineData("rgba(4, 164, 188)", "rgba(4,164,188,0)")]
        [InlineData("hsl(4, 10%, 35%)", "hsl(4,10%,35%)")]
        [InlineData("hsla(4, 10%, 35%,0.55)", "hsla(4,10%,35%,0.55)")]
        [InlineData("hsla(4, 10%, 35%)", "hsla(4,10%,35%,0)")]
        [InlineData("rgb(100%, 12%, 43%)", "rgb(100%,12%,43%)")]
        [InlineData("rgba(100%, 12%, 43%,0.17)", "rgba(100%,12%,43%,0.17)")]
        public void GetColorString_ReadFromCssReader_CorrectColorStringReturned(string colorInCss, string expectedColorInString)
        {
            var definition = new ColorDefinition();
            var reader = new CssReader(colorInCss);

            var colorInString = definition.GetColorString(reader);

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
            var definition = new ColorDefinition();

            var success = definition.TryConvertPercentToOffset(token, out var result);

            success.Should().Be(expectedSuccess);
            result.Should().Be(expectedResult);
        }

        [Theory]
        [InlineData("", 0)]
        [InlineData("s", 0)]
        [InlineData(null, 0)]
        [InlineData("0", 0)]
        [InlineData("0.12", 0.12)]
        [InlineData("1.2", 1.2)]
        public void ToDouble_ConvertingToken_ExpectedResultReturned(string token, float expectedResult)
        {
            var definition = new ColorDefinition();

            var result = definition.ToDouble(token);

            Math.Abs(result - expectedResult).Should().BeLessOrEqualTo(0.001);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(0.12)]
        [InlineData(1.2)]
        public void ToDouble_ConvertingEmptyTokenWithCustomDefault_DefaultValueReturned(float @default)
        {
            var definition = new ColorDefinition();

            var result = definition.ToDouble(null, @default);

            Math.Abs(result - @default).Should().BeLessOrEqualTo(0.001);
        }
    }
}
