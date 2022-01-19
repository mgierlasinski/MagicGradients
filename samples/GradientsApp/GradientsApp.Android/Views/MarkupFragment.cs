using Android.OS;
using Android.Views;
using Android.Widget;
using GradientsApp.Android.Infrastructure;
using GradientsApp.Android.Markup;
using MagicGradients;
using MagicGradients.Markup;
using Microsoft.Maui.Graphics;

namespace GradientsApp.Android.Views
{
    public class MarkupFragment : AppFragment
    {
        public MarkupFragment()
            : base(0, "Markup")
        {
        }

        private View Body() => new LinearLayout(Context).Vertical().Children(new View[]
        {
            new GradientView(Context)
                .Height(160)
                .Source(
                    new LinearGradient()
                        .Stops(Colors.Orange, Colors.Yellow)
                        .Rotate(90)),
            new GradientView(Context)
                .Height(160)
                .Source("linear-gradient(green, orange)")
        });

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            return Body();
        }
    }
}