﻿using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Runtime;
using AndroidX.AppCompat.App;
using GradientsApp.Android.Infrastructure;
using GradientsApp.Android.Views;
using GradientsApp.Infrastructure;
using MagicGradients;
using Fragment = AndroidX.Fragment.App.Fragment;

namespace GradientsApp.Android
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity, IFragmentLoader
    {
        public INavigationService Navigation { get; private set; }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);

            GlobalSetup.Current.UseNativeGradients();
            
            SetContentView(Resource.Layout.activity_main);
            SetupNavigation();
            LoadFragment(new HomeFragment());
        }
        
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);
            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        public void LoadFragment(Fragment fragment)
        {
            var transaction = SupportFragmentManager.BeginTransaction();
            transaction.Replace(Resource.Id.main_fragment, fragment);
            transaction.AddToBackStack(null);
            transaction.Commit();
        }

        private void SetupNavigation()
        {
            Navigation = new NavigationService(this);
            Navigation.RegisterRoute("Linear", typeof(LinearFragment));
        }
    }
}