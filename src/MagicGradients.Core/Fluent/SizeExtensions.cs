using MagicGradients.Converters;

namespace MagicGradients.Fluent
{
    public static class SizeExtensions
    {
        public static IGradientControl Size(this IGradientControl control, Dimensions size)
        {
            control.GradientSize = size;
            return control;
        }

        public static IGradientControl Size(this IGradientControl control, string css)
        {
            var converter = new DimensionsTypeConverter();
            control.GradientSize = (Dimensions)converter.ConvertFromInvariantString(css);

            return control;
        }
    }
}
