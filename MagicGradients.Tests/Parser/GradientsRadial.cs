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
        }

        private Gradient Radial(Action<RadialGradientBuilder> setup = null)
            => new GradientBuilder().AddRadialGradient(setup).Build().First();
    }
}
