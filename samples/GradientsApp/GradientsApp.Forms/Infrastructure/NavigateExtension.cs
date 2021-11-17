using GradientsApp.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Windows.Input;
using Xamarin.Forms;

namespace GradientsApp.Forms.Infrastructure
{
    [ContentProperty(nameof(Route))]
    public class NavigateExtension : Xamarin.Forms.Xaml.IMarkupExtension<ICommand>
    {
        public string Route { get; set; }

        public ICommand ProvideValue(IServiceProvider serviceProvider) => new Command(NavigateToType);
        object Xamarin.Forms.Xaml.IMarkupExtension.ProvideValue(IServiceProvider serviceProvider) => ProvideValue(serviceProvider);

        private void NavigateToType()
        {
            if (Route == null)
            {
                throw new ArgumentNullException(nameof(Route));
            }

            Ioc.Default.GetService<INavigationService>()?.NavigateTo(Route);
        }
    }
}
