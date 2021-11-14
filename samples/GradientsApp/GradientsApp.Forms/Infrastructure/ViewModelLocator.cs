using System;
using System.Globalization;
using Xamarin.Forms;

namespace GradientsApp.Forms.Infrastructure
{
    public class ViewModelLocator
    {
        public static readonly BindableProperty AutoWireViewModelProperty = BindableProperty.CreateAttached(
            "AutoWireViewModel", 
            typeof(bool), 
            typeof(ViewModelLocator), 
            default(bool),
            propertyChanged: OnAutoWireViewModelChanged);

        public static bool GetAutoWireViewModel(BindableObject bindable)
            => (bool)bindable.GetValue(AutoWireViewModelProperty);

        public static void SetAutoWireViewModel(BindableObject bindable, bool value)
            => bindable.SetValue(AutoWireViewModelProperty, value);

        private static void OnAutoWireViewModelChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var view = bindable as Element;

            var viewType = view?.GetType();
            if (viewType?.FullName == null)
            {
                return;
            }

            var viewName = viewType.FullName
                .Replace("GradientsApp.Forms", "GradientsApp")
                .Replace("PlaygroundMaui", "GradientsApp")
                .Replace("Pages", "ViewModels")
                .Replace("Page", "ViewModel");

            var viewModelAssemblyName = typeof(AppSetup).Assembly.FullName;
            var viewModelName = string.Format(CultureInfo.InvariantCulture, "{0}, {1}", viewName, viewModelAssemblyName);

            var viewModelType = Type.GetType(viewModelName);
            if (viewModelType == null)
            {
                return;
            }

            var viewModel = Ioc.Default.GetService(viewModelType);
            view.BindingContext = viewModel;
        }
    }
}
