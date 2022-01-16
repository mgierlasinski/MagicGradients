using MagicGradients.Converters;

namespace MagicGradients.Fluent
{
    public static class RepeatExtensions
    {
        public static IGradientControl Repeat(this IGradientControl control, BackgroundRepeat repeat)
        {
            control.GradientRepeat = repeat;
            return control;
        }

        public static IGradientControl Repeat(this IGradientControl control, string css)
        {
            var converter = new BackgroundRepeatTypeConverter();
            control.GradientRepeat = (BackgroundRepeat)converter.ConvertFromInvariantString(css);

            return control;
        }
    }
}
