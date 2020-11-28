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
        public ICommand GalleryCommand { get; }
        public ICommand AnimationsCommand { get; }
        
        public HomeViewModel()
        {
            LinearCommand = new Command(async () => await Shell.Current.GoToAsync("GradientEditor?id=linear"));
            RadialCommand = new Command(async () => await Shell.Current.GoToAsync("GradientEditor?id=radial"));
            CssCommand = new Command(async () => await Shell.Current.GoToAsync("CssPreviewer"));
            GalleryCommand = new Command(async () => await Shell.Current.GoToAsync("Gallery"));
            AnimationsCommand = new Command(async () => await Shell.Current.GoToAsync("Animations"));
        }
    }
}
