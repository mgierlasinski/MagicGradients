using GradientsApp.Infrastructure;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UIKit;

namespace GradientsApp.iOS.Infrastructure
{
    public class NavigationService : INavigationService
    {
        private readonly Dictionary<string, Type> _routes = new Dictionary<string, Type>();
        private readonly NavigationViewFactory _viewFactory = new NavigationViewFactory();

        private UINavigationController _navigationController;
        protected UINavigationController NavigationController => _navigationController ??= 
            UIApplication.SharedApplication.KeyWindow.RootViewController as UINavigationController;

        public Task NavigateTo(string route)
        {
            if (_routes.TryGetValue(route, out var type))
            {
                var viewController = _viewFactory.CreateInstance<UIViewController>(type);

                if (viewController is IBindableView bindable)
                    _viewFactory.CallEvents(bindable.BindingContext);

                NavigationController.PushViewController(viewController, true);
            }

            return Task.CompletedTask;
        }

        public Task NavigateTo<TParameter>(string route, TParameter parameter)
        {
            if (_routes.TryGetValue(route, out var type))
            {
                var viewController = _viewFactory.CreateInstance<UIViewController>(type);

                if (viewController is IBindableView bindable)
                    _viewFactory.CallEvents(bindable.BindingContext, parameter);

                NavigationController.PushViewController(viewController, true);
            }

            return Task.CompletedTask;
        }

        public void RegisterRoute(string route, Type type)
        {
            _routes.Add(route, type);
        }
    }
}