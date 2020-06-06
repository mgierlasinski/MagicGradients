using FluentAssertions;
using FluentAssertions.Execution;
using MagicGradients.Parser;
using MagicGradients.Parser.TokenDefinitions;
using System.Linq;
using Xamarin.Forms;
using Xunit;

namespace MagicGradients.Tests.Parser.TokenDefinitions
{
    public class ColorNameDefinitionTests
    {
        [Theory]
        [InlineData("red", true)]
        [InlineData("Color.blue", true)]
        [InlineData("green 70%", true)]
        [InlineData("Color.pink.40%", false)]
        public void IsMatch_ProvidedToken_CorrectMatchResult(string token, bool expectedMatch)
        {
            // Arrange
            var definition = new ColorNameDefinition();

            // Act
            var isMatch = definition.IsMatch(token);

            // Assert
            isMatch.Should().Be(expectedMatch);
        }

        [Theory]
        [ClassData(typeof(ColorNameDefinitionData))]
        public void Parse_ValidColor_SingleStopWithColorAndOffset(string color, Color expectedColor, double expectedOffset)
        {
            // Arrange
            var reader = new CssReader(color);
            var builder = new GradientBuilder();
            var definition = new ColorNameDefinition();

            // Act
            definition.Parse(reader, builder);

            // Assert
            var result = builder.Build();

            using (new AssertionScope())
            {
                var stops = result.SelectMany(x => x.Stops).ToArray();
                stops.Should().HaveCount(1);

                stops[0].Color.Should().Be(expectedColor);
                stops[0].Offset.Value.Should().Be(expectedOffset);
            }
        }
    }
}
