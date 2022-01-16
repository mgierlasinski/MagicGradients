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
                    new LinearGradient()
                        .Stops(Colors.Red, Colors.Green)
                        .Rotate(45)
                        .Repeat(),
                    new RadialGradient()
                        .Circle().At(100, 100, OffsetType.Absolute)
                        .Resize(200, 150)
                        .StretchTo(RadialGradientSize.ClosestSide)
                        .Repeat()
                        .Stops(
                            new GradientStop(Colors.Orange, Offset.Prop(0)), 
                            new GradientStop(Colors.Orange, Offset.Prop(0)),
                            new GradientStop(Colors.Orange, Offset.Prop(0))));
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
