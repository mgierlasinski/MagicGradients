using MagicGradients;
using Playground.ViewModels;
using System;
using System.Linq;
using System.Windows.Input;
using Xamarin.Forms;

namespace PlaygroundLite.ViewModels
{
    public class GradientViewModel<TGradient> : BaseViewModel where TGradient : Gradient
    {
        public TGradient Gradient { get; set; }

        public int StopsCount => Gradient.Stops.Count;

        public ICommand AddStopCommand { get; }
        public ICommand RemoveStopCommand { get; set; }

        public GradientViewModel()
        {
            AddStopCommand = new Command(AddColorStop);
            RemoveStopCommand = new Command(RemoveColorStop);
        }

        private void AddColorStop()
        {
            Gradient.Stops.Add(new GradientStop
            {
                Color = GetRandomColor()
            });
            RaisePropertyChanged(nameof(StopsCount));
        }

        private void RemoveColorStop()
        {
            if (Gradient.Stops.Any())
            {
                Gradient.Stops.RemoveAt(Gradient.Stops.Count - 1);
                RaisePropertyChanged(nameof(StopsCount));
            }
        }

        protected Color GetRandomColor()
        {
            var random = new Random();
            return new Color(
                random.NextDouble(),
                random.NextDouble(),
                random.NextDouble());
        }
    }
}
