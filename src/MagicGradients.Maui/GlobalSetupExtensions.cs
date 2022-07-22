namespace MagicGradients.Forms;

public static class GlobalSetupExtensions
{
    public static GlobalSetup UseCssStyles<TControl>(this GlobalSetup setup)
    {
        StyleSheets.RegisterStyle("background", typeof(TControl), nameof(GradientControl.GradientSourceProperty));
        StyleSheets.RegisterStyle("background-size", typeof(TControl), nameof(GradientControl.GradientSizeProperty));
        StyleSheets.RegisterStyle("background-repeat", typeof(TControl), nameof(GradientControl.GradientRepeatProperty));

        return setup;
    }
}