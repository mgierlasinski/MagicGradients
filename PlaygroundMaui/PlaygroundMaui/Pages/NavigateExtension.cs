using System;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PlaygroundMaui.Pages
{
    [ContentProperty(nameof(Type))]
    public class NavigateExtension : IMarkupExtension<ICommand>
    {
        public Type Type { get; set; }

        public ICommand ProvideValue(IServiceProvider serviceProvider) => new Command(NavigateToType);
        object IMarkupExtension.ProvideValue(IServiceProvider serviceProvider) => ProvideValue(serviceProvider);

        private void NavigateToType()
        {
            if (Type == null)
            {
                throw new ArgumentNullException(nameof(Type));
            }

            var page = (Page)Activator.CreateInstance(Type);
            ((App)Application.Current).MainPage.Navigation.PushAsync(page);
        }
    }
}
