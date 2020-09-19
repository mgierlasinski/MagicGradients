using System.Threading.Tasks;
using MagicGradients;
using Xamarin.Forms;
using GradientStop = MagicGradients.GradientStop;

namespace Playground.Views
{
    public partial class SplashPage : ContentPage
    {
        public SplashPage()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
           await Task.WhenAll(
                SetupIoC(),
                PlayGradientAnimation()
            );

            Application.Current.MainPage = new AppShell();
        }

        private Task SetupIoC()
        {
            IoC.RegisterTypes();
            IoC.Initialize();
            return Task.CompletedTask;
        }

        private async Task PlayGradientAnimation()
        {
            var colors = new[]
            {
                Xamarin.Forms.Color.Purple,
                Xamarin.Forms.Color.DodgerBlue,
                Xamarin.Forms.Color.YellowGreen,
                Xamarin.Forms.Color.Crimson,
                Xamarin.Forms.Color.Teal
            };
            var end = 1.0;
            var animationTime = (uint)1000;
            var uiDelayTime = 1200;
            foreach (var color in colors)
            {
                var stop = new GradientStop{ Color = color};
                SplashTarget.Stops.Add(stop);
                var animation = new Animation(v => stop.Offset = new Offset(v, OffsetType.Proportional), end: end);
                animation.Commit(this,"SplashAnimation", length:animationTime);
                await Task.Delay(uiDelayTime);
                end -= 0.2;
            }
        }
    }
}