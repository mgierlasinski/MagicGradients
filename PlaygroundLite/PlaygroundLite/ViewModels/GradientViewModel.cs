using MagicGradients;
using Playground.ViewModels;
using PlaygroundLite.Services;
using System.Linq;
using System.Windows.Input;
using Xamarin.Forms;

namespace PlaygroundLite.ViewModels
{
    public class GradientViewModel<TGradient> : BaseViewModel where TGradient : Gradient
    {
        private TGradient _gradient;
        public TGradient Gradient
        {
            get => _gradient;
            set => SetProperty(ref _gradient, value);
        }

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

        private double _scale = 1;
        public double Scale
        {
            get => _scale;
            set => SetProperty(ref _scale, value, onChanged: UpdateSize);
        }

        public Dimensions Size => Dimensions.Prop(Scale, Scale);

        public BackgroundRepeat[] RepeatItems { get; set; } =
        {
            BackgroundRepeat.Repeat,
            BackgroundRepeat.RepeatX,
            BackgroundRepeat.RepeatY,
            BackgroundRepeat.NoRepeat
        };

        private BackgroundRepeat _selectedRepeat;
        public BackgroundRepeat SelectedRepeat
        {
            get => _selectedRepeat;
            set => SetProperty(ref _selectedRepeat, value);
        }

        public ICommand AddStopCommand { get; }
        public ICommand RemoveStopCommand { get; set; }
        public ICommand ResetCommand { get; set; }

        public GradientViewModel()
        {
            AddStopCommand = new Command(AddColorStop);
            RemoveStopCommand = new Command(RemoveColorStop);
        }

        private void AddColorStop()
        {
            Gradient.Stops.Add(new GradientStop
            {
                Color = ColorUtils.GetRandom()
            });
            UpdateLength();
            UpdateStopsCount();
        }

        private void RemoveColorStop()
        {
            if (Gradient.Stops.Any())
            {
                Gradient.Stops.RemoveAt(Gradient.Stops.Count - 1);
                UpdateLength();
                UpdateStopsCount();
            }
        }

        private void UpdateLength()
        {
            foreach (var stop in Gradient.Stops)
                stop.Offset = Offset.Empty;

            Gradient.Measure(0, 0);

            foreach (var stop in Gradient.Stops)
                stop.Offset = Offset.Prop(stop.RenderOffset * (float)Length);
        }

        protected void UpdateStopsCount()
        {
            RaisePropertyChanged(nameof(StopsCount));
        }

        private void UpdateSize()
        {
            RaisePropertyChanged(nameof(Size));
        }
    }
}
