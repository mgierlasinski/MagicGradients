using GradientsApp.Infrastructure;
using Microsoft.Extensions.DependencyInjection;

namespace GradientsApp.Android.Infrastructure
{
    public class BindableFragment<TViewModel> : AppFragment, IBindableView where TViewModel : class
    {
        public object BindingContext => ViewModel;
        public TViewModel ViewModel { get; }

        public BindableFragment(int layoutId) : base(layoutId)
        {
            ViewModel = Ioc.Default.GetService<TViewModel>();
        }
    }
}