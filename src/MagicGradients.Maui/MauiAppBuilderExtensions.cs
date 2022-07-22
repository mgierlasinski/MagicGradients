using MagicGradients.Forms;
using MagicGradients.Forms.Builder;

namespace MagicGradients.Maui;

public static class MauiAppBuilderExtensions
{
    public static MauiAppBuilder UseMagicGradients(this MauiAppBuilder builder)
    {
        GlobalSetup.Current.UseFactory(new XamlGradientFactory());
        GlobalSetup.Current.UseCssStyles<GradientView>();

        return builder;
    }
}
