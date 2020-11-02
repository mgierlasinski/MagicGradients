using MagicGradients.Toolkit.Controls;

namespace MagicGradients.Toolkit
{
    public static class MagicToolkit
    {
        static MagicToolkit()
        {
            StyleSheets.RegisterStyle("background", typeof(MagicButton), nameof(MagicButton.GradientSourceProperty));
            StyleSheets.RegisterStyle("background-size", typeof(MagicButton), nameof(MagicButton.GradientSizeProperty));
            StyleSheets.RegisterStyle("background-repeat", typeof(MagicButton), nameof(MagicButton.GradientRepeatProperty));
        }
    }
}
