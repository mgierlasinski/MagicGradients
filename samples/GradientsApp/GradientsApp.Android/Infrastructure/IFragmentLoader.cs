using Fragment = AndroidX.Fragment.App.Fragment;

namespace GradientsApp.Android.Infrastructure
{
    public interface IFragmentLoader
    {
        void LoadFragment(Fragment fragment);
    }
}