namespace MagicGradients.Builder
{
    public interface IChildBuilder
    {
        StopsFactory StopsFactory { get; }
        Gradient Construct(IGradientFactory factory);
    }
}
