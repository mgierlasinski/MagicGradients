using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Runtime;
using AndroidX.AppCompat.App;
using MagicGradients;
using MagicGradients.Builder;

namespace GradientsApp.Android
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);

            SetContentView(Resource.Layout.activity_main);
            SetGradient();
        }

        private void SetGradient()
        {
            var magicGradient = FindViewById<GradientView>(Resource.Id.magicGradient);

            magicGradient.GradientSource = new GradientBuilder()
                .AddCssGradient("linear-gradient(43deg, #4158D0 0%, #C850C0 46%, #FFCC70 100%)")
                .ToSource();
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);
            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}