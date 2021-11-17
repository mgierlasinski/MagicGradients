using MagicGradients.Builder;

namespace MagicGradients
{
    public static class GlobalSetupExtensions
    {
        public static GlobalSetup UseXamlGradients(this GlobalSetup setup)
        {
            if (setup.GradientFactory != null)
                return setup;
            return setup.UseFactory(new XamlGradientFactory());
        }

        public static GlobalSetup UseCssStyles<TControl>(this GlobalSetup setup)
        {
            StyleSheets.RegisterStyle("background", typeof(TControl), nameof(GradientControl.GradientSourceProperty));
            StyleSheets.RegisterStyle("background-size", typeof(TControl), nameof(GradientControl.GradientSizeProperty));
            StyleSheets.RegisterStyle("background-repeat", typeof(TControl), nameof(GradientControl.GradientRepeatProperty));

            return setup;
        }
    }
}
