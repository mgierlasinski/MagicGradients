namespace MagicGradients.Builder
{
    public interface IChildBuilder
    {
        StopsFactory StopsFactory { get; }
        IGradient Construct(IGradientFactory factory);
    }
}
