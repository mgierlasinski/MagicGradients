using System;
using System.Globalization;
using FluentAssertions;
using MagicGradients.Parser;
using System.Linq;
using MagicGradients.Parser.TokenDefinitions;
using Xamarin.Forms.Internals;
using Xunit;

namespace MagicGradients.Tests.Parser
{
    public class CssLinearGradientParserTests
    {
        [Theory]
        [MemberData(nameof(CssLinearGradientParserTestData.SimpleGradientData), MemberType = typeof(CssLinearGradientParserTestData))]
        public void ParseCss_SimpleGradientData_CorrectlyParsed(string css, LinearGradient expected)
        {
            var parser = new CssLinearGradientParser();
            var gradient = parser.ParseCss(css).First();

            gradient.Should().BeEquivalentTo(expected);
        }

        [Fact]
        public void ParseCss_ComplexGradientCss_EachGradientHaveCorrectAngleAndStopsCount()
        {
            var css = CssLinearGradientParserTestData.ComplexGradientCss;
            var expectedGradients = CssLinearGradientParserTestData.ComplexLinearGradients;

            var parser = new CssLinearGradientParser();
            var gradients = parser.ParseCss(css);
            gradients.Should().HaveCount(expectedGradients.Length);
            for (var i = 0; i < gradients.Length; i++)
            {
                gradients[i].Angle.Should().Be(expectedGradients[i].Angle);
                gradients[i].Stops.Should().HaveCount(expectedGradients[i].Stops.Count);
            }
        }
    }
}
