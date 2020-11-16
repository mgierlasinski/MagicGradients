using MagicGradients;
using Playground.Extensions;
using Playground.ViewModels;
using System.Linq;
using System.Windows.Input;
using Xamarin.Forms;
using GradientStop = MagicGradients.GradientStop;

namespace Playground.Controls
{
    public class ColorInspectorViewModel : ObservableObject
    {
        private Gradient _gradient;
        public Gradient Gradient
        {
            get => _gradient;
            set => SetProperty(ref _gradient, value);
        }

        private GradientStop _selectedStop;
        public GradientStop SelectedStop
        {
            get => _selectedStop;
            set => SetProperty(ref _selectedStop, value);
        }

        private double _length = 1;
        public double Length
        {
            get => _length;
            set => SetProperty(ref _length, value, UpdateOffsets);
        }

        public ICommand AddStopCommand { get; }
        public ICommand RemoveStopCommand { get; set; }
        public ICommand SelectStopCommand { get; set; }

        public ColorInspectorViewModel()
        {
            AddStopCommand = new Command(AddColorStop);
            RemoveStopCommand = new Command(RemoveColorStop);
            SelectStopCommand = new Command<GradientStop>(s => SelectedStop = s);
        }

        public void OnGradientChanged(Gradient newValue)
        {
            Gradient = newValue;
            SelectedStop = newValue.Stops.First();
        }

        private void AddColorStop()
        {
            Gradient.Stops.Add(new GradientStop
            {
                Color = ColorUtils.GetRandom()
            });
            UpdateOffsets();
        }

        private void RemoveColorStop()
        {
            if (SelectedStop == null || Gradient.Stops.Count == 1)
                return;

            var index = Gradient.Stops.IndexOf(SelectedStop);
            if (index >= 0)
            {
                Gradient.Stops.RemoveAt(index);
                UpdateOffsets();

                SelectedStop = Gradient.Stops.Any() ? Gradient.Stops.First() : null;
            }
        }

        protected void UpdateOffsets()
        {
            foreach (var stop in Gradient.Stops)
                stop.Offset = Offset.Empty;

            // Calculate default RenderOffset
            Gradient.Measure(0, 0);

            foreach (var stop in Gradient.Stops)
                stop.Offset = Offset.Prop(stop.RenderOffset * (float)Length);

            RaisePropertyChanged("Offset");
        }
    }
}
