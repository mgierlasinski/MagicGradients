using MagicGradients;

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

        private int _lengthIndex;
        public int LengthIndex
        {
            get => _lengthIndex;
            set => SetProperty(ref _lengthIndex, value, 
                () => Gradient.IsRepeating = _lengthIndex == 1);
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
    }
}
