using Android.OS;
using Android.Views;
using Fragment = AndroidX.Fragment.App.Fragment;

namespace GradientsApp.Android.Infrastructure
{
    public class AppFragment : Fragment
    {
        private readonly int _layoutId;

        public AppFragment(int layoutId)
        {
            _layoutId = layoutId;
        }
        
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            return inflater.Inflate(_layoutId, container, false);
        }
    }
}