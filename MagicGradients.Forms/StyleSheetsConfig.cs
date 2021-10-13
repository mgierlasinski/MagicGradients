namespace MagicGradients
{
    public static class StyleSheetsConfig
    {
        public static void RegisterStyles<TControl>()
        {
            StyleSheets.RegisterStyle("background", typeof(TControl), nameof(GradientControl.GradientSourceProperty));
            StyleSheets.RegisterStyle("background-size", typeof(TControl), nameof(GradientControl.GradientSizeProperty));
            StyleSheets.RegisterStyle("background-repeat", typeof(TControl), nameof(GradientControl.GradientRepeatProperty));
        }
    }
}
