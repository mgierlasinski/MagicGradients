using System.Collections.Generic;
using System.Linq;
using Microsoft.Maui.Graphics;

namespace MagicGradients.Markup
{
    public static class GradientExtensions
    {
        public static T Stops<T>(this T gradient, params IGradientStop[] stops) where T : Gradient
        {
            gradient.Stops = new List<IGradientStop>(stops);
            return gradient;
        }

        public static T Stops<T>(this T gradient, params Color[] colors) where T : Gradient
        {
            gradient.Stops = colors.Select(x => (IGradientStop)new GradientStop(x)).ToList();
            return gradient;
        }

        public static T Repeat<T>(this T gradient) where T : Gradient
        {
            gradient.IsRepeating = true;
            return gradient;
        }

        public static LinearGradient Rotate(this LinearGradient gradient, double angle)
        {
            gradient.Angle = angle;
            return gradient;
        }

        public static RadialGradient Circle(this RadialGradient gradient)
        {
            gradient.Shape = RadialGradientShape.Circle;
            return gradient;
        }

        public static RadialGradient Ellipse(this RadialGradient gradient)
        {
            gradient.Shape = RadialGradientShape.Ellipse;
            return gradient;
        }

        public static RadialGradient At(this RadialGradient gradient, Position position)
        {
            gradient.Center = position;
            return gradient;
        }

        public static RadialGradient At(this RadialGradient gradient, double x, double y, OffsetType type = OffsetType.Proportional)
        {
            return gradient.At(new Position(x, y, type));
        }

        public static RadialGradient Size(this RadialGradient gradient, Dimensions size)
        {
            gradient.Radius = size;
            return gradient;
        }

        public static RadialGradient Size(this RadialGradient gradient, double width, double height, OffsetType type = OffsetType.Absolute)
        {
            return gradient.Size(new Dimensions(width, height, type));
        }

        public static RadialGradient StretchTo(this RadialGradient gradient, RadialGradientStretch stretch)
        {
            gradient.Stretch = stretch;
            return gradient;
        }
    }
}
