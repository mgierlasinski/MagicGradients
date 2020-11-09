using MagicGradients;
using Playground.Extensions;
using System.Linq;
using System.Windows.Input;
using Xamarin.Forms;
using GradientStop = MagicGradients.GradientStop;

namespace Playground.ViewModels
{
    public class GradientViewModel<TGradient> : ObservableObject where TGradient : Gradient
    {
        private TGradient _gradient;
        public TGradient Gradient
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
            set => SetProperty(ref _length, value, UpdateLength);
        }

        private Dimensions _size = Dimensions.Prop(1, 1);
        public Dimensions Size
        {
            get => _size;
            set => SetProperty(ref _size, value);
        }

        public BackgroundRepeat SelectedRepeat => (BackgroundRepeat)RepeatIndex;

        private int _repeatIndex;
        public int RepeatIndex
        {
            get => _repeatIndex;
            set => SetProperty(ref _repeatIndex, value, () => RaisePropertyChanged(nameof(SelectedRepeat)));
        }

        private int _selectedViewModelIndex;
        public int SelectedViewModelIndex
        {
            get => _selectedViewModelIndex;
            set => SetProperty(ref _selectedViewModelIndex, value);
        }

        public ICommand AddStopCommand { get; }
        public ICommand RemoveStopCommand { get; set; }
        public ICommand ResetCommand { get; set; }
        public ICommand SelectStopCommand { get; set; }

        public GradientViewModel()
        {
            AddStopCommand = new Command(AddColorStop);
            RemoveStopCommand = new Command(RemoveColorStop);
            SelectStopCommand = new Command<GradientStop>(s => SelectedStop = s);
        }

        private void AddColorStop()
        {
            Gradient.Stops.Add(new GradientStop
            {
                Color = ColorUtils.GetRandom()
            });
            UpdateStopsCount();
        }

        private void RemoveColorStop()
        {
            if(SelectedStop == null || Gradient.Stops.Count == 1)
                return;

            var index = Gradient.Stops.IndexOf(SelectedStop);
            if (index >= 0)
            {
                Gradient.Stops.RemoveAt(index);
                UpdateStopsCount();

                SelectedStop = Gradient.Stops.Any() ? Gradient.Stops.First() : null;
            }
        }

        protected void UpdateLength()
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
            UpdateLength();
        }
    }
}
