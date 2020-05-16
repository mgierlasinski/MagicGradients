using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Runtime;
using System;

namespace Playground.Droid
{
    [Activity(Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(savedInstanceState);

#if DEBUG
            RegisterExceptionHanler();
#endif

            global::Xamarin.Forms.Forms.SetFlags("Shell_Experimental", "Visual_Experimental", "CollectionView_Experimental", "FastRenderers_Experimental");
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            LoadApplication(new App());
        }

#if DEBUG
        private void RegisterExceptionHanler()
        {
            AppDomain.CurrentDomain.UnhandledException += (object sender, UnhandledExceptionEventArgs e) =>
            {
                // Record the error in our application logs
                System.Diagnostics.Debug.WriteLine(
                    DateTime.Now.ToString() + "  :  " + "[CRITICAL GLOBALLY HANDLED EXCEPTION] - Critical exception has been hit! - Message: " + e.ExceptionObject +
                        System.Environment.NewLine + System.Environment.NewLine +
                            "========= Critcal Error has been hit, application closed =========" +
                                System.Environment.NewLine + System.Environment.NewLine
                    );
            };
        }
#endif

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}