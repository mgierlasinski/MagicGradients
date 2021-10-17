namespace MagicGradients.Builder
{
    public interface IGradientFactory
    {
        ILinearGradient Construct(LinearGradientBuilder builder);
        IRadialGradient Construct(RadialGradientBuilder builder);
    }
}
