using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;
using static MagicGradients.Parser.CssHelpers;

namespace MagicGradients.Tests.Parser
{
    public class CssGradientParserTestData
    {
        public static string ComplexGradientsCss = "linear-gradient(242deg, rgba(195, 195, 195, 0.02) 0%, rgba(195, 195, 195, 0.02) 16.667%,rgba(91, 91, 91, 0.02) 16.667%, rgba(91, 91, 91, 0.02) 33.334%,rgba(230, 230, 230, 0.02) 33.334%, rgba(230, 230, 230, 0.02) 50.001000000000005%,rgba(18, 18, 18, 0.02) 50.001%, rgba(18, 18, 18, 0.02) 66.668%,rgba(163, 163, 163, 0.02) 66.668%, rgba(163, 163, 163, 0.02) 83.33500000000001%,rgba(140, 140, 140, 0.02) 83.335%, rgba(140, 140, 140, 0.02) 100.002%),linear-gradient(152deg, rgba(151, 151, 151, 0.02) 0%, rgba(151, 151, 151, 0.02) 16.667%,rgba(11, 11, 11, 0.02) 16.667%, rgba(11, 11, 11, 0.02) 33.334%,rgba(162, 162, 162, 0.02) 33.334%, rgba(162, 162, 162, 0.02) 50.001000000000005%,rgba(171, 171, 171, 0.02) 50.001%, rgba(171, 171, 171, 0.02) 66.668%,rgba(119, 119, 119, 0.02) 66.668%, rgba(119, 119, 119, 0.02) 83.33500000000001%,rgba(106, 106, 106, 0.02) 83.335%, rgba(106, 106, 106, 0.02) 100.002%),linear-gradient(11deg, rgba(245, 245, 245, 0.01) 0%, rgba(245, 245, 245, 0.01) 16.667%,rgba(23, 23, 23, 0.01) 16.667%, rgba(23, 23, 23, 0.01) 33.334%,rgba(96, 96, 96, 0.01) 33.334%, rgba(96, 96, 96, 0.01) 50.001000000000005%,rgba(140, 140, 140, 0.01) 50.001%, rgba(140, 140, 140, 0.01) 66.668%,rgba(120, 120, 120, 0.01) 66.668%, rgba(120, 120, 120, 0.01) 83.33500000000001%,rgba(48, 48, 48, 0.01) 83.335%, rgba(48, 48, 48, 0.01) 100.002%),linear-gradient(27deg, rgba(106, 106, 106, 0.03) 0%, rgba(106, 106, 106, 0.03) 14.286%,rgba(203, 203, 203, 0.03) 14.286%, rgba(203, 203, 203, 0.03) 28.572%,rgba(54, 54, 54, 0.03) 28.572%, rgba(54, 54, 54, 0.03) 42.858%,rgba(75, 75, 75, 0.03) 42.858%, rgba(75, 75, 75, 0.03) 57.144%,rgba(216, 216, 216, 0.03) 57.144%, rgba(216, 216, 216, 0.03) 71.42999999999999%,rgba(39, 39, 39, 0.03) 71.43%, rgba(39, 39, 39, 0.03) 85.71600000000001%,rgba(246, 246, 246, 0.03) 85.716%, rgba(246, 246, 246, 0.03) 100.002%),linear-gradient(317deg, rgba(215, 215, 215, 0.01) 0%, rgba(215, 215, 215, 0.01) 16.667%,rgba(72, 72, 72, 0.01) 16.667%, rgba(72, 72, 72, 0.01) 33.334%,rgba(253, 253, 253, 0.01) 33.334%, rgba(253, 253, 253, 0.01) 50.001000000000005%,rgba(4, 4, 4, 0.01) 50.001%, rgba(4, 4, 4, 0.01) 66.668%,rgba(183, 183, 183, 0.01) 66.668%, rgba(183, 183, 183, 0.01) 83.33500000000001%,rgba(17, 17, 17, 0.01) 83.335%, rgba(17, 17, 17, 0.01) 100.002%),linear-gradient(128deg, rgba(119, 119, 119, 0.03) 0%, rgba(119, 119, 119, 0.03) 12.5%,rgba(91, 91, 91, 0.03) 12.5%, rgba(91, 91, 91, 0.03) 25%,rgba(45, 45, 45, 0.03) 25%, rgba(45, 45, 45, 0.03) 37.5%,rgba(182, 182, 182, 0.03) 37.5%, rgba(182, 182, 182, 0.03) 50%,rgba(243, 243, 243, 0.03) 50%, rgba(243, 243, 243, 0.03) 62.5%,rgba(162, 162, 162, 0.03) 62.5%, rgba(162, 162, 162, 0.03) 75%,rgba(190, 190, 190, 0.03) 75%, rgba(190, 190, 190, 0.03) 87.5%,rgba(148, 148, 148, 0.03) 87.5%, rgba(148, 148, 148, 0.03) 100%),linear-gradient(90deg, rgb(185, 139, 80),rgb(176, 26, 6))";

        private static GradientElements<GradientStop> CreateStops(int count)
        {
            return new GradientElements<GradientStop>(Enumerable.Repeat(new GradientStop(), count));
        }

        public static LinearGradient[] ComplexGradientsExpected = new[]
        {
            new LinearGradient{ Angle = FromDegrees(242), Stops = CreateStops(12)},
            new LinearGradient{ Angle = FromDegrees(152), Stops = CreateStops(12)},
            new LinearGradient{ Angle = FromDegrees(11),  Stops = CreateStops(12)},
            new LinearGradient{ Angle = FromDegrees(27),  Stops = CreateStops(14)},
            new LinearGradient{ Angle = FromDegrees(317), Stops = CreateStops(12)},
            new LinearGradient{ Angle = FromDegrees(128), Stops = CreateStops(16)},
            new LinearGradient{ Angle = FromDegrees(90),  Stops = CreateStops(2)}
        }.Reverse().ToArray();

        public static IEnumerable<object[]> SimpleGradients()
        {
            yield return new object[]
            {
                "linear-gradient(rgb(4, 164, 188))", new LinearGradient
                {
                    Angle = 0, Stops = new GradientElements<GradientStop>
                    {
                        new GradientStop { Color = Color.FromRgb(4, 164, 188) }
                    }
                }
            };
            yield return new object[]
            {
                "linear-gradient(90deg, rgb(4, 164, 188))", new LinearGradient
                {
                    Angle = FromDegrees(90), Stops = new GradientElements<GradientStop>
                    {
                        new GradientStop { Color = Color.FromRgb(4, 164, 188) }
                    }
                }
            };
            yield return new object[]
            {
                "linear-gradient(224deg, rgba(155, 155, 155, 0.1) 50%)", new LinearGradient
                {
                    Angle = FromDegrees(224), Stops = new GradientElements<GradientStop>
                    {
                        new GradientStop
                        {
                            Color = Color.FromRgba(0.607843160629272, 0.607843160629272, 0.607843160629272, 0.100000001490116),
                            Offset = 0.5f
                        }
                    }
                }
            };
            yield return new object[]
            {
                "linear-gradient(90deg, hsl(237, 0%, 13%))", new LinearGradient
                {
                    Angle = FromDegrees(90), Stops = new GradientElements<GradientStop>
                    {
                        new GradientStop
                        {
                            Color = Color.FromHsla(0.65833333333333333, 0, 0.13, 1)
                        }
                    }
                }
            };
            yield return new object[]
            {
                "linear-gradient(90deg, rgba(172, 172, 172, 0.01) 100.002%)", new LinearGradient
                {
                    Angle = FromDegrees(90), Stops = new GradientElements<GradientStop>
                    {
                        new GradientStop
                        {
                            Color = Color.FromRgba(0.674509823322296, 0.674509823322296, 0.674509823322296, 0.00999999977648258),
                            Offset = 1
                        }
                    }
                }
            };
        }

        public static IEnumerable<object[]> GradientsWithoutOffsets()
        {
            yield return new object[]
            {
                "linear-gradient(224deg, rgb(0, 0, 0))", new LinearGradient
                {
                    Angle = FromDegrees(224), Stops = new GradientElements<GradientStop>
                    {
                        new GradientStop { Color = Color.Black, RenderOffset = 0f },
                    }
                }
            };
            yield return new object[]
            {
                "linear-gradient(224deg, rgb(0, 0, 0), rgb(0, 0, 0))", new LinearGradient
                {
                    Angle = FromDegrees(224), Stops = new GradientElements<GradientStop>
                    {
                        new GradientStop { Color = Color.Black, RenderOffset = 0f },
                        new GradientStop { Color = Color.Black, RenderOffset = 1f }
                    }
                }
            };
            yield return new object[]
            {
                "linear-gradient(224deg, rgb(0, 0, 0), rgb(0, 0, 0), rgb(0, 0, 0))", new LinearGradient
                {
                    Angle = FromDegrees(224), Stops = new GradientElements<GradientStop>
                    {
                        new GradientStop { Color = Color.Black, RenderOffset = 0f },
                        new GradientStop { Color = Color.Black, RenderOffset = 0.5f },
                        new GradientStop { Color = Color.Black, RenderOffset = 1f }
                    }
                }
            };
        }
    }
}
