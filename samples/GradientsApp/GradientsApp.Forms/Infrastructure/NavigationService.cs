using GradientsApp.Infrastructure;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace GradientsApp.Forms.Infrastructure
{
    public class NavigationService : INavigationService
    {
        private readonly Dictionary<string, Type> _routes = new Dictionary<string, Type>();
        private readonly NavigationViewFactory _viewFactory = new NavigationViewFactory();

        private Page _mainPage;
        protected Page MainPage => _mainPage ??= Application.Current.MainPage;

        public Task NavigateTo(string route)
        {
            if (_routes.TryGetValue(route, out var type))
            {
                var page = _viewFactory.CreateInstance<Page>(type);
                _viewFactory.CallEvents(page.BindingContext);

                return MainPage.Navigation.PushAsync(page);
            }

            return Task.CompletedTask;
        }

        public Task NavigateTo<TParameter>(string route, TParameter parameter)
        {
            if (_routes.TryGetValue(route, out var type))
            {
                var page = _viewFactory.CreateInstance<Page>(type);
                _viewFactory.CallEvents(page.BindingContext, parameter);
                
                return MainPage.Navigation.PushAsync(page);
            }

            return Task.CompletedTask;
        }

        public void RegisterRoute(string route, Type type)
        {
            _routes.Add(route, type);
        }
    }
}
