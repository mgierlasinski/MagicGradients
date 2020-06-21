using FluentAssertions;
using FluentAssertions.Execution;
using MagicGradients.Parser;
using MagicGradients.Tests.Mock;
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
            using (new AssertionScope())
            {
                gradients.Should().NotBeNull();
                gradients.Should().HaveCount(0);
            }
        }

        [Theory]
        [ClassData(typeof(GradientsSimple))]
        public void ParseCss_SimpleGradients_CorrectlyParsed(string css, LinearGradient expected)
        {
            // Arrange
            var parser = new CssGradientParser();

            // Act
            var gradients = parser.ParseCss(css);

            // Assert
            using (new AssertionScope())
            {
                gradients.Should().HaveCount(1);
                gradients[0].Should().BeEquivalentTo(expected, options => options.IgnoringCyclicReferences());
            }
        }

        [Theory]
        [ClassData(typeof(GradientsWithoutOffsets))]
        public void ParseCss_GradientsWithoutOffsets_AutomaticallyAssignedOffsets(string css, LinearGradient expected)
        {
            // Arrange
            var parser = new CssGradientParser();

            // Act
            var gradients = parser.ParseCss(css);
            gradients.ForEach(x => x.Measure(0, 0));

            // Assert
            using (new AssertionScope())
            {
                gradients.Should().HaveCount(1);
                gradients[0].Should().BeEquivalentTo(expected, options => options.IgnoringCyclicReferences());
            }
        }

        [Theory]
        [ClassData(typeof(GradientsComplex))]
        public void ParseCss_ComplexGradientsCss_EachGradientHaveCorrectAngleAndStopsCount(string css, Gradient[] expectedGradients)
        {
            // Arrange
            var parser = new CssGradientParser();

            // Act
            var gradients = parser.ParseCss(css);

            // Assert
            using (new AssertionScope())
            {
                gradients.Should().HaveCount(expectedGradients.Length);

                for (var i = 0; i < gradients.Length; i++)
                {
                    gradients[i].Stops.Should().HaveCount(expectedGradients[i].Stops.Count);
                }
            }
        }
    }
}
