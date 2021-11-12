using GradientsApp.Android.Infrastructure;
using GradientsApp.ViewModels;

namespace GradientsApp.Android.Views
{
    public class CategoriesFragment : BindableFragment<CategoriesViewModel>
    {
        public CategoriesFragment() 
            : base(Resource.Layout.categories_fragment)
        {
        }
    }
}