using System.Windows.Input;
using Xamarin.Forms;

namespace PlaygroundLite.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        public ICommand LinearCommand { get; }
        public ICommand RadialCommand { get; }
        public ICommand CssCommand { get; }
        public ICommand AnimationsSimpleCommand { get; }
        public ICommand AnimationsComplexCommand { get; }

        public MainViewModel()
        {
            LinearCommand = new Command(() => CoreMethods.PushPageModel<LinearViewModel>());
            RadialCommand = new Command(() => CoreMethods.PushPageModel<RadialViewModel>());
            CssCommand = new Command(() => CoreMethods.PushPageModel<CssViewModel>());
            AnimationsSimpleCommand = new Command(() => CoreMethods.PushPageModel<AnimationsSimpleViewModel>());
            AnimationsComplexCommand = new Command(() => CoreMethods.PushPageModel<AnimationsComplexViewModel>());
        }
    }
}
