using FluentAssertions;
using MagicGradients.Parser;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xunit;

namespace MagicGradients.Tests.Parser
{
    public class CssGradientParserTests
    {
        public CssGradientParserTests()
        {
            Device.PlatformServices = new MockPlatformServices();
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        [InlineData(" ")]
        public void ParseCss_EmptyCss_EmptyGradientArrayReturned(string css)
        {
            // Arrange
            var parser = new CssGradientParser();

            // Act
            var gradients = parser.ParseCss(css);

            // Assert
            gradients.Should().NotBeNull();
            gradients.Should().HaveCount(0);
        }

        [Theory]
        [MemberData(nameof(CssGradientParserTestData.SimpleGradients), MemberType = typeof(CssGradientParserTestData))]
        public void ParseCss_SimpleGradients_CorrectlyParsed(string css, LinearGradient expected)
        {
            // Arrange
            var parser = new CssGradientParser();

            // Act
            var gradients = parser.ParseCss(css);

            // Assert
            gradients.Should().HaveCount(1);
            gradients[0].Should().BeEquivalentTo(expected);
        }

        [Theory]
        [MemberData(nameof(CssGradientParserTestData.GradientsWithoutOffsets), MemberType = typeof(CssGradientParserTestData))]
        public void ParseCss_GradientsWithoutOffsets_AutomaticallyAssignedOffsets(string css, LinearGradient expected)
        {
            // Arrange
            var parser = new CssGradientParser();

            // Act
            var gradients = parser.ParseCss(css);
            gradients.ForEach(x => x.Measure());

            // Assert
            gradients.Should().HaveCount(1);
            gradients[0].Should().BeEquivalentTo(expected);
        }

        [Fact]
        public void ParseCss_ComplexGradientsCss_EachGradientHaveCorrectAngleAndStopsCount()
        {
            // Arrange
            var css = CssGradientParserTestData.ComplexGradientsCss;
            var expectedGradients = CssGradientParserTestData.ComplexGradientsExpected;
            var parser = new CssGradientParser();

            // Act
            var gradients = parser.ParseCss(css);

            // Assert
            gradients.Should().HaveCount(expectedGradients.Length);

            for (var i = 0; i < gradients.Length; i++)
            {
                gradients[i].Stops.Should().HaveCount(expectedGradients[i].Stops.Count);
            }
        }
    }
}
