using GradientsApp.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using UIKit;

namespace GradientsApp.iOS.Infrastructure
{
    public class BindableViewController<TViewModel> : UIViewController, IBindableView where TViewModel : class
    {
        public object BindingContext => ViewModel;
        public TViewModel ViewModel { get; }

        public BindableViewController()
        {
            ViewModel = Ioc.Default.GetService<TViewModel>();
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            if (BindingContext is IHaveTitle bindingTitle)
            {
                NavigationController.Title = bindingTitle.Title;
            }
        }
    }
}