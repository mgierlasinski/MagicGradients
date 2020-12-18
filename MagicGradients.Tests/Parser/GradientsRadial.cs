using MagicGradients.Builder;
using System;
using System.Linq;
using Xamarin.Forms;
using Xunit;

namespace MagicGradients.Tests.Parser
{
    public class GradientsRadial : TheoryData<string, Gradient>
    {
        public GradientsRadial()
        {
            Add("radial-gradient(red, green, blue)", Radial(x => x
                .Ellipse()
                .AddStops(Color.Red, Color.Green, Color.Blue)));

            Add("radial-gradient(circle at 80px 80px, yellow, red)", Radial(x => x
                .Circle().At(80, 80, o => o.Absolute())
                .AddStops(Color.Yellow, Color.Red)));

            Add("radial-gradient(ellipse closest-corner at 90% 30%, blue, pink)", Radial(x => x
                .Ellipse().At(0.9, 0.3)
                .StretchTo(RadialGradientSize.ClosestCorner)
                .AddStops(Color.Blue, Color.Pink)));

            Add("radial-gradient(50px 80px at 30% 30%, orange, magenta)", Radial(x => x
                .Ellipse().At(0.3, 0.3)
                .Radius(50, 80)
                .AddStops(Color.Orange, Color.Magenta)));

            Add("radial-gradient(circle 100px at 60% 60%, orange, magenta)", Radial(x => x
                .Circle().At(0.6, 0.6)
                .Radius(100, 100)
                .AddStops(Color.Orange, Color.Magenta)));
        }

        private Gradient Radial(Action<RadialGradientBuilder> setup = null)
            => new GradientBuilder().AddRadialGradient(setup).Build().First();
    }
}
