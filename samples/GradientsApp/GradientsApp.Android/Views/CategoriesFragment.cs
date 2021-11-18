using Android.OS;
using Android.Views;
using AndroidX.RecyclerView.Widget;
using GradientsApp.Android.Infrastructure;
using GradientsApp.Android.Views.Adapters;
using GradientsApp.ViewModels;

namespace GradientsApp.Android.Views
{
    public class CategoriesFragment : BindableFragment<CategoriesViewModel>
    {
        public CategoriesFragment() 
            : base(Resource.Layout.categories_fragment)
        {
        }

        public override void OnViewCreated(View view, Bundle savedInstanceState)
        {
            base.OnViewCreated(view, savedInstanceState);

            var list = view.FindViewById<RecyclerView>(Resource.Id.categories_list);
            list.SetAdapter(new CategoriesAdapter(ViewModel.Categories));
        }
    }
}