using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Runtime;
using Android.Views;
using AndroidX.AppCompat.App;
using GradientsApp.Android.Infrastructure;
using GradientsApp.Android.Views;
using Fragment = AndroidX.Fragment.App.Fragment;

namespace GradientsApp.Android
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity, IFragmentLoader, IToolbarManager
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            
            SetContentView(Resource.Layout.activity_main);
            SupportActionBar.SetDisplayHomeAsUpEnabled(true);

            new App().ConfigureAndRun();
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
        
        public override void OnBackPressed()
        {
            if(SupportFragmentManager.BackStackEntryCount == 1)
                Finish();
            else
                base.OnBackPressed();
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            if (item.ItemId == 16908332)
            {
                OnBackPressed();
                return true;
            }

            return base.OnOptionsItemSelected(item);
        }

        public void SetTitle(string title)
        {
            SupportActionBar.Title = title;
        }

        public void Show()
        {
            SupportActionBar.Show();
        }

        public void Hide()
        {
            SupportActionBar.Hide();
        }
    }
}