using Xamarin.Forms;
using Xunit;
using static MagicGradients.GradientMath;

namespace MagicGradients.Tests.Parser
{
    public class GradientsSimple : TheoryData<string, Gradient>
    {
        public GradientsSimple()
        {
            Add("linear-gradient(rgb(4, 164, 188))", new LinearGradient
            {
                Angle = 0,
                Stops = new GradientElements<GradientStop>
                {
                    new GradientStop { Color = Color.FromRgb(4, 164, 188) }
                }
            });
            
            Add("linear-gradient(90deg, rgb(4, 164, 188))", new LinearGradient
            {
                Angle = FromDegrees(90),
                Stops = new GradientElements<GradientStop>
                {
                    new GradientStop { Color = Color.FromRgb(4, 164, 188) }
                }
            });
            
            Add("linear-gradient(224deg, rgba(155, 155, 155, 0.1) 50%)", new LinearGradient
            {
                Angle = FromDegrees(224),
                Stops = new GradientElements<GradientStop>
                {
                    new GradientStop
                    {
                        Color = Color.FromRgba(0.607843160629272, 0.607843160629272, 0.607843160629272, 0.100000001490116),
                        Offset = new Offset(0.5d, OffsetType.Proportional)
                    }
                }
            });

            Add("linear-gradient(90deg, hsl(237, 0%, 13%))", new LinearGradient
            {
                Angle = FromDegrees(90),
                Stops = new GradientElements<GradientStop>
                {
                    new GradientStop
                    {
                        Color = Color.FromHsla(0.65833333333333333, 0, 0.13, 1)
                    }
                }
            });

            Add("linear-gradient(90deg, rgba(172, 172, 172, 0.01) 100.002%)", new LinearGradient
            {
                Angle = FromDegrees(90),
                Stops = new GradientElements<GradientStop>
                {
                    new GradientStop
                    {
                        Color = Color.FromRgba(0.674509823322296, 0.674509823322296, 0.674509823322296, 0.00999999977648258),
                        Offset = new Offset(1, OffsetType.Proportional)
                    }
                }
            });
        }
    }
}
