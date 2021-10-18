using Microsoft.Maui.Graphics;

namespace MagicGradients.Builder
{
    public interface IGradientFactory
    {
        ILinearGradient Construct(LinearGradientBuilder builder);
        IRadialGradient Construct(RadialGradientBuilder builder);
        IGradientStop CreateStop(Color color, Offset? offset = null);
    }
}
