using Playground.ViewModels;

namespace Playground.Features.Masks
{
    public class MasksViewModel : ObservableObject
    {
        private int _selectedTabIndex;
        public int SelectedTabIndex
        {
            get => _selectedTabIndex;
            set => SetProperty(ref _selectedTabIndex, value);
        }
    }
}
