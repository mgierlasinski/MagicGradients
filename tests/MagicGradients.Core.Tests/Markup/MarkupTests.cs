using FluentAssertions;
using MagicGradients.Markup;
using MagicGradients.Masks;
using Microsoft.Maui.Graphics;
using Xunit;

namespace MagicGradients.Core.Tests.Markup
{
    [Trait("Feature", "Markup")]
    public class MarkupTests
    {
        [Fact]
        public void SourceExtension_UseParamsOverload_SetupSource()
        {
            // Arrange
            var view = new GradientView()
                .Source(
                    new LinearGradient()
                        .Stops(Colors.Red, Colors.Green)
                        .Rotate(45)
                        .Repeat(),
                    new RadialGradient()
                        .Circle().At(100, 100, OffsetType.Absolute)
                        .Size(200, 150)
                        .StretchTo(RadialGradientStretch.ClosestSide)
                        .Repeat()
                        .Stops(
                            new GradientStop(Colors.Orange, Offset.Prop(0)), 
                            new GradientStop(Colors.Blue, Offset.Prop(0.6)),
                            new GradientStop(Colors.Chocolate, Offset.Prop(1))));

            // Assert
            view.GradientSource.GetGradients().Should().HaveCount(2);
        }

        [Fact]
        public void SourceExtension_UseBuilderOverload_SetupSource()
        {
            // Arrange
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

            // Assert
            view.GradientSource.GetGradients().Should().HaveCount(3);
        }

        [Fact]
        public void ViewExtensions_CssSourceWithSizeAndRepeat_SetupSource()
        {
            // Arrange
            var view = new GradientView()
                .Source("linear-gradient(red, green)")
                .Size(20, 20)
                .Repeat(BackgroundRepeat.RepeatX);

            // Assert
            view.GradientSource.GetGradients().Should().HaveCount(1);
        }

        [Fact]
        public void MaskExtension_UseParamsOverload_SetupMaskCollection()
        {
            // Arrange
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

            // Assert
            view.Mask.Should().BeOfType<MaskCollection>();
        }
    }
}
