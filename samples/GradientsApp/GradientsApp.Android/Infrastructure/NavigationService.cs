using GradientsApp.Infrastructure;
using Microsoft.Maui.ApplicationModel;
using Fragment = AndroidX.Fragment.App.Fragment;

namespace GradientsApp.Android.Infrastructure
{
    public class NavigationService : INavigationService
    {
        private readonly Dictionary<string, Type> _routes = new Dictionary<string, Type>();
        private readonly NavigationViewFactory _viewFactory = new NavigationViewFactory();

        private IFragmentLoader? _fragmentLoader;
        protected IFragmentLoader? FragmentLoader => _fragmentLoader ??= Platform.CurrentActivity as IFragmentLoader;
        
        public Task NavigateTo(string route)
        {
            if (_routes.TryGetValue(route, out var type))
            {
                var fragment = _viewFactory.CreateInstance<Fragment>(type);

                if (fragment is IBindableView bindable)
                    _viewFactory.CallEvents(bindable.BindingContext);

                FragmentLoader?.LoadFragment(fragment);
            }

            return Task.CompletedTask;
        }

        public Task NavigateTo<TParameter>(string route, TParameter parameter)
        {
            if (_routes.TryGetValue(route, out var type))
            {
                var fragment = _viewFactory.CreateInstance<Fragment>(type);

                if (fragment is IBindableView bindable)
                    _viewFactory.CallEvents(bindable.BindingContext, parameter);

                FragmentLoader?.LoadFragment(fragment);
            }

            return Task.CompletedTask;
        }

        public void RegisterRoute(string route, Type type)
        {
            _routes.Add(route, type);
        }
    }
}