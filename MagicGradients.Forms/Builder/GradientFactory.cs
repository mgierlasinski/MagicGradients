using Microsoft.Maui.Graphics;
using System.Linq;

namespace MagicGradients.Builder
{
    public class GradientFactory : IGradientFactory
    {
        public ILinearGradient Construct(LinearGradientBuilder builder)
        {
            var linearGradient = new LinearGradient
            {
                Angle = builder.Angle,
                IsRepeating = builder.IsRepeating,
                Stops = new GradientElements<GradientStop>(builder.Stops.Cast<GradientStop>())
            };

            return linearGradient;
        }

        public IRadialGradient Construct(RadialGradientBuilder builder)
        {
            var radialGradient = new RadialGradient
            {
                Center = builder.Center,
                Shape = builder.Shape,
                Size = builder.Size,
                RadiusX = builder.RadiusX,
                RadiusY = builder.RadiusY,
                Flags = builder.Flags,
                IsRepeating = builder.IsRepeating,
                Stops = new GradientElements<GradientStop>(builder.Stops.Cast<GradientStop>())
            };

            return radialGradient;
        }

        public IGradientStop CreateStop(Color color, Offset? offset = null)
        {
            var stop = new GradientStop
            {
                Color = color,
                Offset = offset ?? Offset.Empty
            };

            return stop;
        }
    }
}
