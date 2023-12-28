using Android.Views;
using AndroidX.AppCompat.App;
using GradientsApp.Android.Infrastructure;
using GradientsApp.Android.Views;
using Microsoft.Maui.ApplicationModel;
using Fragment = AndroidX.Fragment.App.Fragment;

namespace GradientsApp.Android;

[Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
public class MainActivity : AppCompatActivity, IFragmentLoader, IToolbarManager
{
    protected override void OnCreate(Bundle? savedInstanceState)
    {
        base.OnCreate(savedInstanceState);
        Platform.Init(this, savedInstanceState);

        SetContentView(Resource.Layout.activity_main);
        SupportActionBar.SetDisplayHomeAsUpEnabled(true);

        App.ConfigureAndRun();
        LoadFragment(new HomeFragment());
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
        if (SupportFragmentManager.BackStackEntryCount == 1)
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