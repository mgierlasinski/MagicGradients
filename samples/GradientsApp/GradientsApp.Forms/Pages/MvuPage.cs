using MagicGradients;
using MagicGradients.Fluent;
using Microsoft.Maui.Graphics;
using System.Collections.Generic;
using Xamarin.Forms;
using GradientView = MagicGradients.Forms.GradientView;

namespace GradientsApp.Forms.Pages
{
    public class MvuPage : ContentPage
    {
        public MvuPage()
        {
            Content = CreateContent();
        }

        private List<View> Body() => new List<View>
        {
            new GradientView()
                .Source(
                    new LinearGradient()
                        .Stops(Colors.Orange, Colors.Yellow)
                        .Rotate(90)),
            new GradientView()
                .Source("linear-gradient(green, orange)")
        };

        private View CreateContent()
        {
            var stack = new StackLayout();
            var children = Body();

            foreach (var child in children)
            {
                child.HeightRequest = 160;
                stack.Children.Add(child);
            }

            return stack;
        }
    }
}