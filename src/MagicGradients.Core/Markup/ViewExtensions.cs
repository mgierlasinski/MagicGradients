using System;
using System.Collections.Generic;
using MagicGradients.Builder;
using MagicGradients.Masks;

namespace MagicGradients.Markup
{
    public static class ViewExtensions
    {
        public static T Source<T>(this T control, IGradientSource source) where T : IGradientControl
        {
            control.GradientSource = source;
            return control;
        }

        public static T Source<T>(this T control, params IGradient[] gradients) where T : IGradientControl
        {
            control.GradientSource = new GenericGradientSource(gradients);
            return control;
        }

        public static T Source<T>(this T control, string css) where T : IGradientControl
        {
            control.GradientSource = new CssGradientSource(css);
            return control;
        }

        public static T Source<T>(this T control, Action<GradientBuilder> build) where T : IGradientControl
        {
            var builder = new GradientBuilder();
            build.Invoke(builder);
            control.GradientSource = builder.BuildSource();

            return control;
        }

        public static T Mask<T>(this T control, IGradientMask mask) where T : IGradientControl
        {
            control.Mask = mask;
            return control;
        }

        public static T Mask<T>(this T control, params IGradientMask[] masks) where T : IGradientControl
        {
            control.Mask = new MaskCollection { Masks = new List<IGradientMask>(masks) };
            return control;
        }

        public static T GradientSize<T>(this T control, Dimensions size) where T : IGradientControl
        {
            control.GradientSize = size;
            return control;
        }

        public static T GradientSize<T>(this T control, double width, double height, OffsetType type = OffsetType.Absolute) where T : IGradientControl
        {
            control.GradientSize = new Dimensions(width, height, type);
            return control;
        }

        public static T GradientRepeat<T>(this T control, BackgroundRepeat repeat) where T : IGradientControl
        {
            control.GradientRepeat = repeat;
            return control;
        }
    }
}
