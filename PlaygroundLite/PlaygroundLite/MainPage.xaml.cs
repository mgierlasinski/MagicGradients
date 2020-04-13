using MagicGradients;
using System;
using System.Linq;
using Xamarin.Forms;

namespace PlaygroundLite
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private void AddStop_Clicked(object sender, EventArgs e)
        {
            var linearGradient = (LinearGradient)GradientView.GradientSource;
            var random = new Random();

            linearGradient.Stops.Add(new GradientStop()
            {
                Color = new Color(
                    random.NextDouble(),
                    random.NextDouble(),
                    random.NextDouble())
            });
        }

        private void RemoveStop_Clicked(object sender, EventArgs e)
        {
            var linearGradient = (LinearGradient)GradientView.GradientSource;
            if (linearGradient.Stops.Any())
            {
                linearGradient.Stops.RemoveAt(linearGradient.Stops.Count - 1);
            }
        }

        private void RotateLeft_Clicked(object sender, EventArgs e)
        {
            var linearGradient = (LinearGradient)GradientView.GradientSource;
            linearGradient.Angle -= 10;
        }

        private void RotateRight_Clicked(object sender, EventArgs e)
        {
            var linearGradient = (LinearGradient)GradientView.GradientSource;
            linearGradient.Angle += 10;
        }
    }
}
