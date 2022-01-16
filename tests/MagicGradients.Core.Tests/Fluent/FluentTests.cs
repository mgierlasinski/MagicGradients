using MagicGradients.Fluent;
using MagicGradients.Masks;
using Microsoft.Maui.Graphics;

namespace MagicGradients.Core.Tests.Fluent
{
    public class FluentTests
    {
        public void SourceParams()
        {
            var view = new GradientView()
                .Source(
                    new LinearGradient(),
                    new RadialGradient());
        }

        public void SourceBuilder()
        {
            var view = new GradientView()
                .Source(b => b
                    .AddLinearGradient(o => o
                        .AddStops(Colors.Red, Colors.Blue))
                    .AddRadialGradient()
                    .AddCssGradient("xxx"));
        }

        public void MaskParams()
        {
            var view = new GradientView()
                .Mask(
                    new RectangleMask(),
                    new TextMask());
        }
    }
}
