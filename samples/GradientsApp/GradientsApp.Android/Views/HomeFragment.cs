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

            Toolbar.Hide();

            var linear = view.FindViewById<Button>(Resource.Id.home_linear);
            linear.Click += (sender, args) => ViewModel.NavigateCommand.Execute(AppRoutes.Linear);

            var radial = view.FindViewById<Button>(Resource.Id.home_radial);
            radial.Click += (sender, args) => ViewModel.NavigateCommand.Execute(AppRoutes.Radial);

            var masks = view.FindViewById<Button>(Resource.Id.home_masks);
            masks.Click += (sender, args) => ViewModel.NavigateCommand.Execute(AppRoutes.Masks);

            var gallery = view.FindViewById<Button>(Resource.Id.home_gallery);
            gallery.Click += (sender, args) => ViewModel.NavigateCommand.Execute(AppRoutes.Categories);
        }
    }
}