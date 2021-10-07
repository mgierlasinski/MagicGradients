using System;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace PlaygroundMaui.Infrastructure
{
    public interface INavigationService
    {
        Task NavigateTo(Type type);
        Task NavigateTo<TPage, TParameter>(TParameter parameter);
    }

    public class NavigationService : INavigationService
    {
        private readonly Page _mainPage;

        public NavigationService(Page mainPage)
        {
            _mainPage = mainPage;
        }

        public Task NavigateTo(Type type)
        {
            var page = CreateInstance(type);
            return _mainPage.Navigation.PushAsync(page);
        }

        public Task NavigateTo<TPage, TParameter>(TParameter parameter)
        {
            var page = CreateInstance(typeof(TPage));
            if (page.BindingContext is INavigationAware<TParameter> navAware)
            {
                navAware.Prepare(parameter);
            }
            return _mainPage.Navigation.PushAsync(page);
        }

        private Page CreateInstance(Type type)
        {
            return (Page)Activator.CreateInstance(type);
        }
    }
}
