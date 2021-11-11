namespace GradientsApp.Android.Infrastructure
{
    public interface IBindableFragment
    {
        object BindingContext { get; }
    }

    public interface IBindableFragment<TViewModel> : IBindableFragment
    {
        public TViewModel ViewModel { get; }
    }
}