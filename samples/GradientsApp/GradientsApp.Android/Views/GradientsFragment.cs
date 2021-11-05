using Android.OS;
using Android.Views;
using GradientsApp.ViewModels;
using MagicGradients;
using Fragment = AndroidX.Fragment.App.Fragment;

namespace GradientsApp.Android.Views
{
    public class GradientsFragment : Fragment
    {
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            return inflater.Inflate(Resource.Layout.gradients_fragment, container, false);
        }

        public override void OnViewCreated(View view, Bundle savedInstanceState)
        {
            base.OnViewCreated(view, savedInstanceState);
            SetGradients(view);
        }

        private void SetGradients(View view)
        {
            var vm = new GradientsViewModel();
            
            view.FindViewById<GradientView>(Resource.Id.magicBurst).GradientSource = vm.Burst;
            view.FindViewById<GradientView>(Resource.Id.magicAngular).GradientSource = vm.Angular;
            view.FindViewById<GradientView>(Resource.Id.magicRainbow).GradientSource = vm.Rainbow;
            view.FindViewById<GradientView>(Resource.Id.magicDiamonds).GradientSource = vm.Diamonds;
        }
    }
}