namespace GradientsApp.Android.Infrastructure
{
    public interface IBindableFragment
    {
        object BindingContext { get; }
    }

    public class BindableFragment<TViewModel> : AppFragment, IBindableFragment where TViewModel : class
    {
        public object BindingContext => ViewModel;
        public TViewModel ViewModel { get; }

        public BindableFragment(int layoutId) : base(layoutId)
        {
            ViewModel = AppSetup.IoC.GetInstance<TViewModel>();
        }
    }
}