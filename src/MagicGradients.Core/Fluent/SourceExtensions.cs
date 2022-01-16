using MagicGradients.Builder;
using System;

namespace MagicGradients.Fluent
{
    public static class SourceExtensions
    {
        public static IGradientControl Source(this IGradientControl control, IGradientSource source)
        {
            control.GradientSource = source;
            return control;
        }

        public static IGradientControl Source(this IGradientControl control, params IGradient[] gradients)
        {
            control.GradientSource = new GenericGradientSource(gradients);
            return control;
        }

        public static IGradientControl Source(this IGradientControl control, string css)
        {
            control.GradientSource = new CssGradientSource(css);
            return control;
        }

        public static IGradientControl Source(this IGradientControl control, Action<GradientBuilder> build)
        {
            var builder = new GradientBuilder();
            build.Invoke(builder);
            control.GradientSource = builder.BuildSource();

            return control;
        }
    }
}
