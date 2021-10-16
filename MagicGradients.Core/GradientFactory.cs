using MagicGradients.Builder;

namespace MagicGradients
{
    public class GradientFactory : IGradientFactory
    {
        public ILinearGradient Construct(LinearGradientBuilder builder)
        {
            var linearGradient = new LinearGradient
            {
                Angle = builder.Angle,
                IsRepeating = builder.IsRepeating,
                Stops = new GradientElements<GradientStop>(builder.StopsFactory.Stops)
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
                Stops = new GradientElements<GradientStop>(builder.StopsFactory.Stops)
            };

            return radialGradient;
        }
    }
}
