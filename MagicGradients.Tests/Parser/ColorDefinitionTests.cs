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
    }
}
