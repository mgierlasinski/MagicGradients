using FluentAssertions;
using MagicGradients.Parser;
using Xunit;

namespace MagicGradients.Tests.Parser
{
    public class CssLinearGradientParserTests
    {
        [Theory]
        [MemberData(nameof(CssLinearGradientParserTestData.SimpleGradients), MemberType = typeof(CssLinearGradientParserTestData))]
        public void ParseCss_SimpleGradients_CorrectlyParsed(string css, LinearGradient expected)
        {
            // Arrange
            var parser = new CssLinearGradientParser();

            // Act
            var gradients = parser.ParseCss(css);

            // Assert
            gradients.Should().HaveCount(1);
            gradients[0].Should().BeEquivalentTo(expected);
        }

        [Theory]
        [MemberData(nameof(CssLinearGradientParserTestData.GradientsWithoutOffsets), MemberType = typeof(CssLinearGradientParserTestData))]
        public void ParseCss_GradientsWithoutOffsets_AutomaticallyAssignedOffsets(string css, LinearGradient expected)
        {
            // Arrange
            var parser = new CssLinearGradientParser();

            // Act
            var gradients = parser.ParseCss(css);

            // Assert
            gradients.Should().HaveCount(1);
            gradients[0].Should().BeEquivalentTo(expected);
        }

        [Fact]
        public void ParseCss_ComplexGradientsCss_EachGradientHaveCorrectAngleAndStopsCount()
        {
            // Arrange
            var css = CssLinearGradientParserTestData.ComplexGradientsCss;
            var expectedGradients = CssLinearGradientParserTestData.ComplexGradientsExpected;
            var parser = new CssLinearGradientParser();

            // Act
            var gradients = parser.ParseCss(css);

            // Assert
            gradients.Should().HaveCount(expectedGradients.Length);

            for (var i = 0; i < gradients.Length; i++)
            {
                gradients[i].Angle.Should().Be(expectedGradients[i].Angle);
                gradients[i].Stops.Should().HaveCount(expectedGradients[i].Stops.Count);
            }
        }
    }
}
