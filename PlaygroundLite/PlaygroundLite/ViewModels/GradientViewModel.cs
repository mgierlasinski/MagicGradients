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

        private bool _isRepeating;
        public bool IsRepeating
        {
            get => _isRepeating;
            set => SetProperty(ref _isRepeating, value, 
                onChanged: () => Gradient.IsRepeating = _isRepeating);
        }

        private double _length = 1;
        public double Length
        {
            get => _length;
            set => SetProperty(ref _length, value, onChanged: UpdateLength);
        }

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
            UpdateLength();
            RaisePropertyChanged(nameof(StopsCount));
        }

        private void RemoveColorStop()
        {
            if (Gradient.Stops.Any())
            {
                Gradient.Stops.RemoveAt(Gradient.Stops.Count - 1);
                UpdateLength();
                RaisePropertyChanged(nameof(StopsCount));
            }
        }

        private void UpdateLength()
        {
            foreach (var stop in Gradient.Stops)
                stop.Offset = -1;

            Gradient.Measure(0, 0);

            foreach (var stop in Gradient.Stops)
                stop.Offset = stop.RenderOffset * (float)Length;
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
