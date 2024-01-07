namespace MagicGradients.Maui.Skia;

public static class MauiAppBuilderExtensions
{
    public static MauiAppBuilder UseMagicGradientsWithSkia(this MauiAppBuilder builder)
    {
        GlobalSetup.Current.UseCssStyles<SkiaGradientView>();
        GlobalSetup.Current.UseCssStyles<SkiaGradientGLView>();

        return builder.UseMagicGradients();
    }
}
