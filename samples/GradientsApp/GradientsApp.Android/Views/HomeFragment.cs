using Android.OS;
using Android.Views;
using Android.Widget;
using GradientsApp.Android.Infrastructure;
using GradientsApp.ViewModels;
using Xamarin.Essentials;

namespace GradientsApp.Android.Views
{
    public class HomeFragment : AppFragment, IBindableFragment<HomeViewModel>
    {
        public object BindingContext => ViewModel;
        public HomeViewModel ViewModel { get; }
        
        public HomeFragment() 
            : base(Resource.Layout.home_fragment)
        {
            var navigationService = (Platform.CurrentActivity as IFragmentLoader)?.Navigation;
            ViewModel = new HomeViewModel(navigationService);
        }
        
        public override void OnViewCreated(View view, Bundle savedInstanceState)
        {
            base.OnViewCreated(view, savedInstanceState);

            var button = view.FindViewById<Button>(Resource.Id.home_linear);
            button.Click += (sender, args) => ViewModel.LinearCommand.Execute(null);
        }
    }
}