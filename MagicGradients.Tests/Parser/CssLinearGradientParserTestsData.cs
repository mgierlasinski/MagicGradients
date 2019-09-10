using System.Collections.Generic;
using Xamarin.Forms;

namespace MagicGradients.Tests.Parser
{
    public class CssLinearGradientParserTestData
    {
        public static IEnumerable<object[]> SimpleGradientData()
        {
            yield return new object[]
            {
                "linear-gradient(rgb(4, 164, 188))", new LinearGradient
                {
                    Angle = 0, Stops = new List<LinearGradientStop>
                    {
                        new LinearGradientStop { Color = Color.FromRgb(4, 164, 188) }
                    }
                }
            };
            yield return new object[]
            {
                "linear-gradient(90deg, rgb(4, 164, 188))", new LinearGradient
                {
                    Angle = 270, Stops = new List<LinearGradientStop>
                    {
                        new LinearGradientStop { Color = Color.FromRgb(4, 164, 188) }
                    }
                }
            };
            yield return new object[]
            {
                "linear-gradient(224deg, rgba(155, 155, 155, 0.1) 50%)", new LinearGradient
                {
                    Angle = 44, Stops = new List<LinearGradientStop>
                    {
                        new LinearGradientStop
                        {
                            Color = Color.FromRgba(155, 155, 155, 26),
                            Offset = 0.5f
                        }
                    }
                }
            };
            yield return new object[]
            {
                "linear-gradient(90deg, hsl(237, 0%, 13%))", new LinearGradient
                {
                    Angle = 270, Stops = new List<LinearGradientStop>
                    {
                        new LinearGradientStop { Color = Color.FromHsla(237, 0, 13, 1) }
                    }
                }
            };
            yield return new object[]
            {
                "linear-gradient(90deg, rgba(172, 172, 172, 0.01) 100.002%)", new LinearGradient
                {
                    Angle = 270, Stops = new List<LinearGradientStop>
                    {
                        new LinearGradientStop
                        {
                            Color = Color.FromRgba(172, 172, 172, 3),
                            Offset = 1
                        }
                    }
                }
            };
        }
    }
}
