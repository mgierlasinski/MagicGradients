using MagicGradients;
using MagicGradients.Markup;
using Microsoft.Maui.Graphics;
using Xamarin.CommunityToolkit.Markup;
using Xamarin.Forms;
using GradientView = MagicGradients.Forms.GradientView;

namespace GradientsApp.Forms.Pages
{
    public class MarkupPage : ContentPage
    {
        public MarkupPage()
        {
            Title = "Markup";
            Content = Body();
        }

        private View Body() => new StackLayout { Children = 
        {
            new GradientView()
                .Height(160)
                .Source(
                    new LinearGradient()
                        .Stops(Colors.Orange, Colors.Yellow)
                        .Rotate(90)),
            new GradientView()
                .Height(160)
                .Source("linear-gradient(green, orange)")
        }};
    }
}