using System;
using System.Globalization;
using FluentAssertions;
using MagicGradients.Parser;
using System.Linq;
using Xamarin.Forms.Internals;
using Xunit;

namespace MagicGradients.Tests.Parser
{
    public class CssFormsLinearGradientParserTests
    {
        [Theory]
        [MemberData(nameof(CssLinearGradientParserTestData.SimpleGradientData), MemberType = typeof(CssLinearGradientParserTestData))]
        public void FormsParseCss_SimpleGradientData_CorrectlyParsed(string css, LinearGradient expected)
        {
            var parser = new CssFormsLinearGradientParser();
            var gradient = parser.ParseCss(css).First();

            gradient.Should().BeEquivalentTo(expected);
        }

        static double ParseColorValue(string elem, int maxValue, bool acceptPercent)
        {
            elem = elem.Trim();
            if (elem.EndsWith("%", StringComparison.Ordinal) && acceptPercent)
            {
                maxValue = 100;
                elem = elem.Substring(0, elem.Length - 1);
            }
            return (double)(int.Parse(elem, NumberStyles.Number, CultureInfo.InvariantCulture).Clamp(0, maxValue)) / maxValue;
        }

        [Fact]
        public void FormsParseCss_ComplexGradientCss_EachGradientHaveCorrectAngleAndStopsCount()
        {
            var css = CssLinearGradientParserTestData.ComplexGradientCss;
            var expectedGradients = CssLinearGradientParserTestData.ComplexLinearGradients;

            var parser = new CssFormsLinearGradientParser();
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
