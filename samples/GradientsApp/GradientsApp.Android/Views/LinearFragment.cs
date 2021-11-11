using Android.OS;
using Android.Views;
using GradientsApp.Android.Infrastructure;
using GradientsApp.ViewModels;
using MagicGradients;

namespace GradientsApp.Android.Views
{
    public class LinearFragment : AppFragment, IBindableFragment<LinearViewModel>
    {
        public object BindingContext => ViewModel;
        public LinearViewModel ViewModel { get; }

        public LinearFragment() 
            : base(Resource.Layout.gradients_fragment)
        {
            ViewModel = new LinearViewModel();
        }
        
        public override void OnViewCreated(View view, Bundle savedInstanceState)
        {
            base.OnViewCreated(view, savedInstanceState);
            SetGradients(view);
        }

        private void SetGradients(View view)
        {
            view.FindViewById<GradientView>(Resource.Id.magicBurst).GradientSource = ViewModel.Burst;
            view.FindViewById<GradientView>(Resource.Id.magicAngular).GradientSource = ViewModel.Angular;
            //view.FindViewById<GradientView>(Resource.Id.magicRainbow).GradientSource = ViewModel.Rainbow;
            view.FindViewById<GradientView>(Resource.Id.magicDiamonds).GradientSource = ViewModel.Diamonds;
        }
    }
}