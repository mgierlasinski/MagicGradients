using Android.OS;
using Android.Views;
using GradientsApp.Infrastructure;
using Microsoft.Extensions.DependencyInjection;

namespace GradientsApp.Android.Infrastructure
{
    public class BindableFragment<TViewModel> : AppFragment, IBindableView where TViewModel : class
    {
        public object BindingContext => ViewModel;
        public TViewModel ViewModel { get; }

        public BindableFragment(int layoutId, string title = null) 
            : base(layoutId, title)
        {
            ViewModel = Ioc.ServiceProvider.GetService<TViewModel>();
        }

        public override void OnViewCreated(View view, Bundle savedInstanceState)
        {
            base.OnViewCreated(view, savedInstanceState);

            if (BindingContext is IHaveTitle bindingTitle)
            {
                Toolbar.SetTitle(bindingTitle.Title);
            }
        }
    }
}