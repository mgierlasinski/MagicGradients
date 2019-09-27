using FluentAssertions;
using FluentAssertions.Execution;
using MagicGradients.Parser;
using MagicGradients.Parser.TokenDefinitions;
using System.Linq;
using Xamarin.Forms;
using Xunit;

namespace MagicGradients.Tests.Parser.TokenDefinitions
{
    public class ColorChannelDefinitionTests
    {
        [Theory]
        [InlineData("rgb", true)]
        [InlineData("Rgba", true)]
        [InlineData("HSL", true)]
        [InlineData("hsLa", true)]
        [InlineData("rgbb", false)]
        [InlineData("HSLla", false)]
        public void IsMatch_ProvidedToken_CorrectMatchResult(string token, bool expectedMatch)
        {
            // Arrange
            var definition = new ColorChannelDefinition();

            // Act
            var isMatch = definition.IsMatch(token);

            // Assert
            isMatch.Should().Be(expectedMatch);
        }

        [Theory]
        [MemberData(nameof(ColorChannelDefinitionTestsData.ColorParseData), MemberType = typeof(ColorChannelDefinitionTestsData))]
        public void Parse_ValidColor_SingleStopWithColorAndOffset(string color, Color expectedColor, float expectedOffset)
        {
            // Arrange
            var reader = new CssReader(color);
            var builder = new GradientBuilder();
            var definition = new ColorChannelDefinition();

            // Act
            definition.Parse(reader, builder);

            // Assert
            var result = builder.Build();

            using (new AssertionScope())
            {
                var stops = result.SelectMany(x => x.Stops).ToArray();
                stops.Should().HaveCount(1);

                stops[0].Color.Should().Be(expectedColor);
                stops[0].Offset.Should().Be(expectedOffset);
            }
        }

        [Theory]
        [InlineData("rgb(4, 164, 188)")]
        [InlineData("rgba(4, 164, 188, 0.1)")]
        [InlineData("hsl(4, 10%, 35%)")]
        [InlineData("hsla(4, 10%, 35%,0.55)")]
        [InlineData("rgb(100%, 12%, 43%)")]
        [InlineData("rgba(100%, 12%, 43%,0.17)")]
        public void GetColorString_ReadFromCssReader_CorrectColorStringReturned(string colorInCss)
        {
            // Arrange
            var definition = new ColorChannelDefinition();
            var reader = new CssReader(colorInCss);

            // Act
            var colorInString = definition.GetColorString(reader);

            // Assert
            colorInString.Should().Be(colorInCss);
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
            var definition = new ColorChannelDefinition();

            // Act
            var success = definition.TryConvertPercentToOffset(token, out var result);

            // Assert
            success.Should().Be(expectedSuccess);
            result.Should().Be(expectedResult);
        }
    }
}
