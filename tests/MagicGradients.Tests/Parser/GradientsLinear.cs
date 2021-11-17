using MagicGradients.Builder;
using System;
using System.Linq;
using Xamarin.Forms;
using Xunit;
using static MagicGradients.GradientMath;

namespace MagicGradients.Tests.Parser
{
    public class GradientsLinear : TheoryData<string, Gradient>
    {
        public GradientsLinear()
        {
            Add("linear-gradient(rgb(4, 164, 188))", Linear(x => x
                .Rotate(0)
                .AddStop(Color.FromRgb(4, 164, 188))));

            Add("linear-gradient(90deg, rgb(4, 164, 188))", Linear(x => x
                .Rotate(FromDegrees(90))
                .AddStop(Color.FromRgb(4, 164, 188))));

            Add("linear-gradient(224deg, rgba(155, 155, 155, 0.1) 50%)", Linear(x => x
                .Rotate(FromDegrees(224))
                .AddStop(
                    Color.FromRgba(0.607843160629272, 0.607843160629272, 0.607843160629272, 0.100000001490116),
                    Offset.Prop(0.5d))));

            Add("linear-gradient(90deg, hsl(237, 0%, 13%))", Linear(x => x
                .Rotate(FromDegrees(90))
                .AddStop(Color.FromHsla(0.65833333333333333, 0, 0.13, 1))));

            Add("linear-gradient(90deg, rgba(172, 172, 172, 0.01) 100.002%)", Linear(x => x
                .Rotate(FromDegrees(90))
                .AddStop(
                    Color.FromRgba(0.674509823322296, 0.674509823322296, 0.674509823322296, 0.00999999977648258),
                    Offset.Prop(1))));
        }

        private Gradient Linear(Action<LinearGradientBuilder> setup = null) 
            => new GradientBuilder().AddLinearGradient(setup).Build().First();
    }
}
