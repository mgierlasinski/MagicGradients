using MagicGradients.Builder;

namespace MagicGradients.Maui.Builder;

public class XamlGradientFactory : IGradientFactory
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
            Stretch = builder.Stretch,
            Radius = builder.Radius,
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
