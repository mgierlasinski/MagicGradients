using Android.OS;
using Android.Views;
using Android.Widget;
using GradientsApp.Android.Infrastructure;
using GradientsApp.ViewModels;

namespace GradientsApp.Android.Views
{
    public class HomeFragment : BindableFragment<HomeViewModel>
    {
        public HomeFragment() 
            : base(Resource.Layout.home_fragment)
        {
        }
        
        public override void OnViewCreated(View view, Bundle savedInstanceState)
        {
            base.OnViewCreated(view, savedInstanceState);

            var button = view.FindViewById<Button>(Resource.Id.home_linear);
            button.Click += (sender, args) => ViewModel.LinearCommand.Execute(null);
        }
    }
}