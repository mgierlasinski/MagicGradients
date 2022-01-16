using MagicGradients.Builder;
using MagicGradients.Masks;
using System;
using System.Collections.Generic;

namespace MagicGradients.Fluent
{
    public static class ViewExtensions
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

        public static IGradientControl Mask(this IGradientControl control, IGradientMask mask)
        {
            control.Mask = mask;
            return control;
        }

        public static IGradientControl Mask(this IGradientControl control, params IGradientMask[] masks)
        {
            control.Mask = new MaskCollection { Masks = new List<IGradientMask>(masks) };
            return control;
        }

        public static IGradientControl Size(this IGradientControl control, Dimensions size)
        {
            control.GradientSize = size;
            return control;
        }

        public static IGradientControl Size(this IGradientControl control, double width, double height, OffsetType type = OffsetType.Absolute)
        {
            control.GradientSize = new Dimensions(width, height, type);
            return control;
        }

        public static IGradientControl Repeat(this IGradientControl control, BackgroundRepeat repeat)
        {
            control.GradientRepeat = repeat;
            return control;
        }
    }
}
