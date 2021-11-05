using Android.OS;
using Android.Views;
using MagicGradients;
using MagicGradients.Builder;
using MagicGradients.Masks;
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
            //new GradientBuilder()
            //    .AddLinearGradient(g => g
            //        .Rotate(45)
            //        .AddStops(Colors.Red, Colors.Orange, Colors.Yellow))
            //    .BuildFor(FindViewById<GradientView>(Resource.Id.magicGradient));

            new GradientBuilder()
                .AddCssGradient("linear-gradient(45deg, rgba(22, 31, 43, 0.5) 0%, rgba(22, 31, 43, 0.5) 12.5%,rgba(53, 28, 54, 0.5) 12.5%, rgba(53, 28, 54, 0.5) 25%,rgba(83, 25, 65, 0.5) 25%, rgba(83, 25, 65, 0.5) 37.5%,rgba(114, 22, 76, 0.5) 37.5%, rgba(114, 22, 76, 0.5) 50%,rgba(144, 20, 86, 0.5) 50%, rgba(144, 20, 86, 0.5) 62.5%,rgba(175, 17, 97, 0.5) 62.5%, rgba(175, 17, 97, 0.5) 75%,rgba(205, 14, 108, 0.5) 75%, rgba(205, 14, 108, 0.5) 87.5%,rgba(236, 11, 119, 0.5) 87.5%, rgba(236, 11, 119, 0.5) 100%),linear-gradient(135deg, rgb(188, 0, 159) 0%, rgb(188, 0, 159) 12.5%,rgb(173, 4, 150) 12.5%, rgb(173, 4, 150) 25%,rgb(158, 7, 141) 25%, rgb(158, 7, 141) 37.5%,rgb(143, 11, 132) 37.5%, rgb(143, 11, 132) 50%,rgb(129, 15, 124) 50%, rgb(129, 15, 124) 62.5%,rgb(114, 19, 115) 62.5%, rgb(114, 19, 115) 75%,rgb(99, 22, 106) 75%, rgb(99, 22, 106) 87.5%,rgb(84, 26, 97) 87.5%, rgb(84, 26, 97) 100%)")
                .BuildFor(view.FindViewById<GradientView>(Resource.Id.magicGradient));

            new GradientBuilder()
                .AddCssGradient("linear-gradient(43deg, #4158D0 0%, #C850C0 46%, #FFCC70 100%)")
                .BuildFor(view.FindViewById<GradientView>(Resource.Id.magicRainbow));

            new GradientBuilder()
                .AddCssGradient("linear-gradient(201deg, rgba(148, 148, 148, 0.07) 0%, rgba(148, 148, 148, 0.07) 50%,rgba(83, 83, 83, 0.07) 50%, rgba(83, 83, 83, 0.07) 100%),linear-gradient(192deg, rgba(176, 176, 176, 0.08) 0%, rgba(176, 176, 176, 0.08) 50%,rgba(180, 180, 180, 0.08) 50%, rgba(180, 180, 180, 0.08) 100%),linear-gradient(48deg, rgba(185, 185, 185, 0.1) 0%, rgba(185, 185, 185, 0.1) 50%,rgba(243, 243, 243, 0.1) 50%, rgba(243, 243, 243, 0.1) 100%),linear-gradient(65deg, rgba(172, 172, 172, 0.09) 0%, rgba(172, 172, 172, 0.09) 50%,rgba(209, 209, 209, 0.09) 50%, rgba(209, 209, 209, 0.09) 100%),linear-gradient(4deg, rgba(224, 224, 224, 0.03) 0%, rgba(224, 224, 224, 0.03) 50%,rgba(49, 49, 49, 0.03) 50%, rgba(49, 49, 49, 0.03) 100%),linear-gradient(228deg, rgba(152, 152, 152, 0.03) 0%, rgba(152, 152, 152, 0.03) 50%,rgba(130, 130, 130, 0.03) 50%, rgba(130, 130, 130, 0.03) 100%),linear-gradient(163deg, rgba(170, 170, 170, 0.08) 0%, rgba(170, 170, 170, 0.08) 50%,rgba(232, 232, 232, 0.08) 50%, rgba(232, 232, 232, 0.08) 100%),linear-gradient(152deg, rgba(12, 12, 12, 0.05) 0%, rgba(12, 12, 12, 0.05) 50%,rgba(161, 161, 161, 0.05) 50%, rgba(161, 161, 161, 0.05) 100%),linear-gradient(302deg, rgba(48, 48, 48, 0.1) 0%, rgba(48, 48, 48, 0.1) 50%,rgba(195, 195, 195, 0.1) 50%, rgba(195, 195, 195, 0.1) 100%),linear-gradient(90deg, rgb(144, 14, 253),rgb(74, 115, 255))")
                .BuildFor(view.FindViewById<GradientView>(Resource.Id.magicAngular));

            var maskView = view.FindViewById<GradientView>(Resource.Id.pathMask);
            maskView.Mask = new PathMask
            {
                Data = "M 0 -100 L 58.8 90.9, -95.1 -30.9, 95.1 -30.9, -58.8 80.9 Z",
                Stretch = Stretch.AspectFit
            };

            new GradientBuilder()
                .AddCssGradient("linear-gradient(142deg, rgba(250, 250, 250, 0.05) 0%, rgba(250, 250, 250, 0.05) 53%,rgba(64, 64, 64, 0.05) 53%, rgba(64, 64, 64, 0.05) 100%),linear-gradient(29deg, rgba(10, 10, 10, 0.05) 0%, rgba(10, 10, 10, 0.05) 27%,rgba(94, 94, 94, 0.05) 27%, rgba(94, 94, 94, 0.05) 100%),linear-gradient(118deg, rgba(4, 4, 4, 0.05) 0%, rgba(4, 4, 4, 0.05) 18%,rgba(188, 188, 188, 0.05) 18%, rgba(188, 188, 188, 0.05) 100%),linear-gradient(90deg, rgb(10, 143, 251),rgb(35, 61, 210))")
                .BuildFor(maskView);
        }
    }
}