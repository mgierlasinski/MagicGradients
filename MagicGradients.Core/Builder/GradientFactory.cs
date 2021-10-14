namespace MagicGradients.Builder
{
    public interface IGradientFactory
    {
        LinearGradient Construct(LinearGradientBuilder builder);
        RadialGradient Construct(RadialGradientBuilder builder);
    }

    public class GradientFactory : IGradientFactory
    {
        public LinearGradient Construct(LinearGradientBuilder builder)
        {
            var linearGradient = new LinearGradient
            {
                Angle = builder.Angle,
                IsRepeating = builder.IsRepeating,
                Stops = new GradientElements<GradientStop>(builder.StopsFactory.Stops)
            };

            return linearGradient;
        }

        public RadialGradient Construct(RadialGradientBuilder builder)
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
