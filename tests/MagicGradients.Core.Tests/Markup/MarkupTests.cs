using MagicGradients.Markup;
using MagicGradients.Masks;
using Microsoft.Maui.Graphics;

namespace MagicGradients.Core.Tests.Markup
{
    public class MarkupTests
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
                            new GradientStop(Colors.Blue, Offset.Prop(0.6)),
                            new GradientStop(Colors.Chocolate, Offset.Prop(1))));
        }

        public void SourceBuilder()
        {
            var view = new GradientView()
                .Source(b => b
                    .AddLinearGradient(o => o
                        .AddStops(Colors.Red, Colors.Blue)
                        .Rotate(90)
                        .Repeat())
                    .AddRadialGradient(o => o
                        .Ellipse().At(0.5, 0.5)
                        .AddStop(Colors.Pink, Offset.Prop(0))
                        .AddStop(Colors.Red, Offset.Prop(0.3))
                        .AddStop(Colors.Magenta, Offset.Prop(1)))
                    .AddCssGradient("linear-gradient(red, green)"));
        }

        public void SourceCss()
        {
            var view = new GradientView()
                .Source("linear-gradient(red, green)")
                .Size(20, 20)
                .Repeat(BackgroundRepeat.RepeatX);
        }

        public void MaskParams()
        {
            var view = new GradientView()
                .Mask(
                    new RectangleMask()
                        .Size(100, 100)
                        .Corners(topRight: 20, bottomLeft: 50),
                    new EllipseMask()
                        .Size(0.5, 0.5, OffsetType.Proportional),
                    new PathMask("M 0 -100 L 58.8 90.9, -95.1 -30.9, 95.1 -30.9, -58.8 80.9 Z")
                        .Stretch(Stretch.AspectFit),
                    new TextMask("Magic Gradients")
                        .FontSize(20)
                        .FontAttributes(FontAttributes.Bold)
                        .VerticalTextAlignment(TextAlignment.Start)
                        .HorizontalTextAlignment(TextAlignment.End)
                        .ClipMode(ClipMode.Exclude));
        }
    }
}
