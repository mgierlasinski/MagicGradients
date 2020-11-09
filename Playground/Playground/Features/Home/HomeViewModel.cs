using Playground.ViewModels;
using System.Windows.Input;
using Xamarin.Forms;

namespace Playground.Features.Home
{
    public class HomeViewModel : ObservableObject
    {
        public ICommand LinearCommand { get; }
        public ICommand RadialCommand { get; }
        public ICommand CssCommand { get; }
        public ICommand AnimationsSimpleCommand { get; }
        public ICommand AnimationsComplexCommand { get; }

        public HomeViewModel()
        {
            LinearCommand = new Command(async () => await Shell.Current.GoToAsync("LinearGradient"));
            RadialCommand = new Command(async () => await Shell.Current.GoToAsync("RadialGradient"));
            CssCommand = new Command(() => {});
            AnimationsSimpleCommand = new Command(() => {});
            AnimationsComplexCommand = new Command(() => {});
        }
    }
}
