using GradientsApp.Infrastructure;
using Fragment = AndroidX.Fragment.App.Fragment;

namespace GradientsApp.Android.Infrastructure
{
    public interface IFragmentLoader
    {
        INavigationService Navigation { get; }
        void LoadFragment(Fragment fragment);
    }
}