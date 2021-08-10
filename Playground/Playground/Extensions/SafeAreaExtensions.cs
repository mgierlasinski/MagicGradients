using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using Application = Xamarin.Forms.Application;
using Page = Xamarin.Forms.Page;

namespace Playground.Extensions
{
    public static class SafeAreaExtensions
    {
        public static void InitSafeAreaInsets(this Page page)
        {
            var safeInsets = page.On<iOS>().SafeAreaInsets();
            safeInsets.Top = 0;
            safeInsets.Bottom = safeInsets.Bottom * -1;

            Application.Current.Resources["SafeAreaInsets"] = safeInsets;
        }

        public static Thickness GetSafeAreaInsets(this Page page)
        {
            if (Application.Current.Resources.TryGetValue("SafeAreaInsets", out var safeInsets))
            {
                return (Thickness)safeInsets;
            }

            return new Thickness();
        }
    }
}
