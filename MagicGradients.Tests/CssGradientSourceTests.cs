using FluentAssertions;
using FluentAssertions.Execution;
using MagicGradients.Builder;
using MagicGradients.Tests.Mock;
using System.Linq;
using Xamarin.Forms;
using Xunit;

namespace MagicGradients.Tests
{
    public class CssGradientSourceTests
    {
        public CssGradientSourceTests()
        {
            Device.PlatformServices = new MockPlatformServices();
        }

        [Fact]
        public void SetStylesheetProperty_CssParsedAsGradients()
        {
            // Arrange
            var css = "linear-gradient(red, green)";
            var expected = new GradientBuilder()
                .AddLinearGradient(g => g.AddStops(Color.Red, Color.Green))
                .Build().First();

            // Act
            var source = new CssGradientSource();
            source.Stylesheet = css;

            // Assert
            AssertSourceHasExpected(source, expected);
        }

        [Fact]
        public void UseStaticParseMethod_CssParsedAsGradients()
        {
            // Arrange
            var css = "radial-gradient(blue, yellow)";
            var expected = new GradientBuilder()
                .AddRadialGradient(g => g.AddStops(Color.Blue, Color.Yellow))
                .Build().First();

            // Act
            var source = CssGradientSource.Parse(css);

            // Assert
            AssertSourceHasExpected(source, expected);
        }

        private void AssertSourceHasExpected(CssGradientSource source, Gradient expected)
        {
            using (new AssertionScope())
            {
                source.Gradients.Should().HaveCount(1);
                source.Gradients[0].Should().BeEquivalentTo(expected, options => options
                    .Excluding(o => o.Parent)
                    .IgnoringCyclicReferences());
            }
        }
    }
}
