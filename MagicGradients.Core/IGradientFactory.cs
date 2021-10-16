using MagicGradients.Builder;

namespace MagicGradients
{
    public interface IGradientFactory
    {
        ILinearGradient Construct(LinearGradientBuilder builder);
        IRadialGradient Construct(RadialGradientBuilder builder);
    }
}
