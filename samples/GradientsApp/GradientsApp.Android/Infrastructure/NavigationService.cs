using GradientsApp.Infrastructure;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Fragment = AndroidX.Fragment.App.Fragment;

namespace GradientsApp.Android.Infrastructure
{
    public class NavigationService : INavigationService
    {
        private readonly Dictionary<string, Type> _routes = new Dictionary<string, Type>();

        private IFragmentLoader _fragmentLoader;
        protected IFragmentLoader FragmentLoader => _fragmentLoader ??= Platform.CurrentActivity as IFragmentLoader;
        
        public Task NavigateTo(string route)
        {
            if (_routes.TryGetValue(route, out var type))
            {
                var fragment = CreateInstance(type);
                FragmentLoader.LoadFragment(fragment);
            }

            return Task.CompletedTask;
        }

        public Task NavigateTo<TParameter>(string route, TParameter parameter)
        {
            if (_routes.TryGetValue(route, out var type))
            {
                var fragment = CreateInstance(type);

                if (fragment is IBindableFragment {BindingContext: INavigationAware<TParameter> vm})
                {
                    vm.Prepare(parameter);
                }

                FragmentLoader.LoadFragment(fragment);
            }

            return Task.CompletedTask;
        }

        public void RegisterRoute(string route, Type type)
        {
            _routes.Add(route, type);
        }

        private Fragment CreateInstance(Type type)
        {
            return (Fragment)Activator.CreateInstance(type);
        }
    }
}