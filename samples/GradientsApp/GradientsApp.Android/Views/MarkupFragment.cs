using Android.OS;
using Android.Views;
using Android.Widget;
using GradientsApp.Android.Infrastructure;
using GradientsApp.Android.Markup;
using MagicGradients;
using MagicGradients.Markup;
using MagicGradients.Masks;
using Microsoft.Maui.Graphics;
using GradientStop = MagicGradients.GradientStop;

namespace GradientsApp.Android.Views
{
    public class MarkupFragment : AppFragment
    {
        private const int GHeight = 300;

        public MarkupFragment()
            : base(0, "Markup")
        {
        }

        private View Body() => new LinearLayout(Context).Vertical().Children(new View[]
        {
            new GradientView(Context)
                .Height(GHeight)
                .Source(
                    new LinearGradient()
                        .Stops(Colors.Orange, Colors.Yellow)
                        .Rotate(45)),
            new GradientView(Context)
                .Height(GHeight)
                .Source(
                    new RadialGradient()
                        .Circle().At(100, 100, OffsetType.Absolute)
                        .Size(200, 130)
                        .Repeat()
                        .Stops(
                            new GradientStop(Colors.Orange, Offset.Prop(0)),
                            new GradientStop(Colors.Blue, Offset.Prop(0.6)),
                            new GradientStop(Colors.Chocolate, Offset.Prop(1)))),
            new GradientView(Context)
                .Height(GHeight)
                .Source(b => b
                    .AddRadialGradient(o => o
                        .Ellipse().At(0.5, 0.5)
                        .AddStop(Colors.Pink, Offset.Prop(0))
                        .AddStop(Colors.Red, Offset.Prop(0.3))
                        .AddStop(Colors.Magenta, Offset.Prop(1)))),
            new GradientView(Context)
                .Height(GHeight)
                .Source("linear-gradient(43deg, #4158D0 0%, #C850C0 46%, #FFCC70 100%)")
                .GradientSize(GHeight, GHeight)
                .GradientRepeat(BackgroundRepeat.RepeatX),
            new GradientView(Context)
                .Height(GHeight)
                .Source("linear-gradient(242deg, red, green, orange)")
                .Mask(
                    new PathMask("M 0 -100 L 58.8 90.9, -95.1 -30.9, 95.1 -30.9, -58.8 80.9 Z")
                        .Stretch(Stretch.AspectFit))
        });

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            return new ScrollView(Context).Children(Body());
        }
    }
}