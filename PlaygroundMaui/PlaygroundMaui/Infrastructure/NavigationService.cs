using System;
using Xamarin.Forms;

namespace PlaygroundMaui.Infrastructure
{
    public class NavigationService
    {
        private readonly Page _mainPage;

        public NavigationService(Page mainPage)
        {
            _mainPage = mainPage;
        }

        public void NavigateTo(Type type)
        {
            var page = CreateInstance(type);
            _mainPage.Navigation.PushAsync(page);
        }

        public void NavigateTo<TPage, TParameter>(TParameter parameter)
        {
            var page = CreateInstance(typeof(TPage));
            if (page.BindingContext is INavigationAware<TParameter> navAware)
            {
                navAware.Prepare(parameter);
            }
            _mainPage.Navigation.PushAsync(page);
        }

        private Page CreateInstance(Type type)
        {
            return (Page)Activator.CreateInstance(type);
        }
    }
}
