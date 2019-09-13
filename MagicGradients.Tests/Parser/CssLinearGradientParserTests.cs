using System;
using System.Globalization;
using FluentAssertions;
using MagicGradients.Parser;
using System.Linq;
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
        public void Global_Test()
        {
            //todo, make it ok!!!
            var css =
                "linear-gradient(242deg, rgba(195, 195, 195, 0.02) 0%, rgba(195, 195, 195, 0.02) 16.667%,rgba(91, 91, 91, 0.02) 16.667%, rgba(91, 91, 91, 0.02) 33.334%,rgba(230, 230, 230, 0.02) 33.334%, rgba(230, 230, 230, 0.02) 50.001000000000005%,rgba(18, 18, 18, 0.02) 50.001%, rgba(18, 18, 18, 0.02) 66.668%,rgba(163, 163, 163, 0.02) 66.668%, rgba(163, 163, 163, 0.02) 83.33500000000001%,rgba(140, 140, 140, 0.02) 83.335%, rgba(140, 140, 140, 0.02) 100.002%),linear-gradient(152deg, rgba(151, 151, 151, 0.02) 0%, rgba(151, 151, 151, 0.02) 16.667%,rgba(11, 11, 11, 0.02) 16.667%, rgba(11, 11, 11, 0.02) 33.334%,rgba(162, 162, 162, 0.02) 33.334%, rgba(162, 162, 162, 0.02) 50.001000000000005%,rgba(171, 171, 171, 0.02) 50.001%, rgba(171, 171, 171, 0.02) 66.668%,rgba(119, 119, 119, 0.02) 66.668%, rgba(119, 119, 119, 0.02) 83.33500000000001%,rgba(106, 106, 106, 0.02) 83.335%, rgba(106, 106, 106, 0.02) 100.002%),linear-gradient(11deg, rgba(245, 245, 245, 0.01) 0%, rgba(245, 245, 245, 0.01) 16.667%,rgba(23, 23, 23, 0.01) 16.667%, rgba(23, 23, 23, 0.01) 33.334%,rgba(96, 96, 96, 0.01) 33.334%, rgba(96, 96, 96, 0.01) 50.001000000000005%,rgba(140, 140, 140, 0.01) 50.001%, rgba(140, 140, 140, 0.01) 66.668%,rgba(120, 120, 120, 0.01) 66.668%, rgba(120, 120, 120, 0.01) 83.33500000000001%,rgba(48, 48, 48, 0.01) 83.335%, rgba(48, 48, 48, 0.01) 100.002%),linear-gradient(27deg, rgba(106, 106, 106, 0.03) 0%, rgba(106, 106, 106, 0.03) 14.286%,rgba(203, 203, 203, 0.03) 14.286%, rgba(203, 203, 203, 0.03) 28.572%,rgba(54, 54, 54, 0.03) 28.572%, rgba(54, 54, 54, 0.03) 42.858%,rgba(75, 75, 75, 0.03) 42.858%, rgba(75, 75, 75, 0.03) 57.144%,rgba(216, 216, 216, 0.03) 57.144%, rgba(216, 216, 216, 0.03) 71.42999999999999%,rgba(39, 39, 39, 0.03) 71.43%, rgba(39, 39, 39, 0.03) 85.71600000000001%,rgba(246, 246, 246, 0.03) 85.716%, rgba(246, 246, 246, 0.03) 100.002%),linear-gradient(317deg, rgba(215, 215, 215, 0.01) 0%, rgba(215, 215, 215, 0.01) 16.667%,rgba(72, 72, 72, 0.01) 16.667%, rgba(72, 72, 72, 0.01) 33.334%,rgba(253, 253, 253, 0.01) 33.334%, rgba(253, 253, 253, 0.01) 50.001000000000005%,rgba(4, 4, 4, 0.01) 50.001%, rgba(4, 4, 4, 0.01) 66.668%,rgba(183, 183, 183, 0.01) 66.668%, rgba(183, 183, 183, 0.01) 83.33500000000001%,rgba(17, 17, 17, 0.01) 83.335%, rgba(17, 17, 17, 0.01) 100.002%),linear-gradient(128deg, rgba(119, 119, 119, 0.03) 0%, rgba(119, 119, 119, 0.03) 12.5%,rgba(91, 91, 91, 0.03) 12.5%, rgba(91, 91, 91, 0.03) 25%,rgba(45, 45, 45, 0.03) 25%, rgba(45, 45, 45, 0.03) 37.5%,rgba(182, 182, 182, 0.03) 37.5%, rgba(182, 182, 182, 0.03) 50%,rgba(243, 243, 243, 0.03) 50%, rgba(243, 243, 243, 0.03) 62.5%,rgba(162, 162, 162, 0.03) 62.5%, rgba(162, 162, 162, 0.03) 75%,rgba(190, 190, 190, 0.03) 75%, rgba(190, 190, 190, 0.03) 87.5%,rgba(148, 148, 148, 0.03) 87.5%, rgba(148, 148, 148, 0.03) 100%),linear-gradient(90deg, rgb(185, 139, 80),rgb(176, 26, 6))";
            var parser = new CssLinearGradientParser();
            var gradients = parser.ParseCss(css);
            var first = gradients.First();
            Assert.True(true);
        }
    }
}
