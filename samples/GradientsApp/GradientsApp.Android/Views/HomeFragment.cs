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

            AddNavigation(Resource.Id.home_linear, AppRoutes.Linear, view);
            AddNavigation(Resource.Id.home_radial, AppRoutes.Radial, view);
            AddNavigation(Resource.Id.home_masks, AppRoutes.Masks, view);
            AddNavigation(Resource.Id.home_gallery, AppRoutes.Categories, view);
            AddNavigation(Resource.Id.home_markup, AppRoutes.Markup, view);
        }

        private void AddNavigation(int id, string route, View view)
        {
            var button = view.FindViewById<Button>(id);
            button.Click += (_, _) => ViewModel.NavigateCommand.Execute(route);
        }
    }
}