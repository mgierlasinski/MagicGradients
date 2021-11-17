using Xamarin.Forms;
using Xunit;
using static MagicGradients.GradientMath;

namespace MagicGradients.Tests.Parser
{
    public class GradientsWithoutOffsets : TheoryData<string, Gradient>
    {
        public GradientsWithoutOffsets()
        {
            Add("linear-gradient(224deg, rgb(0, 0, 0))", new LinearGradient
            {
                Angle = FromDegrees(224),
                Stops = new GradientElements<GradientStop>
                {
                    new GradientStop { Color = Color.Black, RenderOffset = 0f },
                }
            });

            Add("linear-gradient(224deg, rgb(0, 0, 0), rgb(0, 0, 0))", new LinearGradient
            {
                Angle = FromDegrees(224),
                Stops = new GradientElements<GradientStop>
                {
                    new GradientStop { Color = Color.Black, RenderOffset = 0f },
                    new GradientStop { Color = Color.Black, RenderOffset = 1f }
                }
            });
            
            Add("linear-gradient(224deg, rgb(0, 0, 0), rgb(0, 0, 0), rgb(0, 0, 0))", new LinearGradient
            {
                Angle = FromDegrees(224),
                Stops = new GradientElements<GradientStop>
                {
                    new GradientStop { Color = Color.Black, RenderOffset = 0f },
                    new GradientStop { Color = Color.Black, RenderOffset = 0.5f },
                    new GradientStop { Color = Color.Black, RenderOffset = 1f }
                }
            });
        }
    }
}
