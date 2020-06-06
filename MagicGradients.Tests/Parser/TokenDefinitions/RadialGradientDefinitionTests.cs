using FluentAssertions;
using FluentAssertions.Execution;
using MagicGradients.Parser;
using MagicGradients.Parser.TokenDefinitions;
using Xunit;

namespace MagicGradients.Tests.Parser.TokenDefinitions
{
    public class RadialGradientDefinitionTests
    {
        [Theory]
        [ClassData(typeof(RadialGradientDefinitionData))]
        public void Parse_ValidGradientCss_ExpectedGradientReturned(string css, RadialGradient expectedGradient)
        {
            // Arrange
            var reader = new CssReader(css);
            var builder = new GradientBuilder();
            var definition = new RadialGradientDefinition();

            // Act
            definition.Parse(reader, builder);

            // Assert
            var result = builder.Build();

            using (new AssertionScope())
            {
                result.Should().HaveCount(1);
                var gradient = result[0] as RadialGradient;
                gradient.Should().NotBeNull();
                gradient.Should().BeEquivalentTo(expectedGradient);
            }
        }
    }
}
