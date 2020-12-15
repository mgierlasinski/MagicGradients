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
                .AddStops(Color.Red, Color.Green, Color.Blue)));

            Add("radial-gradient(50px 50px at 30% 30%, orange, magenta)", Radial(x => x
                .Ellipse().At(0.3, 0.3, o => o.Proportional())
                .Radius(50, 50, o => o.Absolute())
                .AddStops(Color.Orange, Color.Magenta)));
        }

        private Gradient Radial(Action<RadialGradientBuilder> setup = null)
            => new GradientBuilder().AddRadialGradient(setup).Build().First();
    }
}
