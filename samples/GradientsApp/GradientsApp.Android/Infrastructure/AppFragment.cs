using Android.OS;
using Android.Views;
using Xamarin.Essentials;
using Fragment = AndroidX.Fragment.App.Fragment;

namespace GradientsApp.Android.Infrastructure
{
    public class AppFragment : Fragment
    {
        private readonly int _layoutId;
        private readonly string _title;

        private IToolbarManager _toolbar;
        protected IToolbarManager Toolbar => _toolbar ??= Platform.CurrentActivity as IToolbarManager;

        public AppFragment(int layoutId, string title = null)
        {
            _layoutId = layoutId;
            _title = title;
        }
        
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            return inflater.Inflate(_layoutId, container, false);
        }

        public override void OnViewCreated(View view, Bundle savedInstanceState)
        {
            Toolbar.Show();
            Toolbar.SetTitle(_title);
        }
    }
}