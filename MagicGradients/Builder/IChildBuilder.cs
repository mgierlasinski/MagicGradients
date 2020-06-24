namespace MagicGradients
{
    public interface IChildBuilder
    {
        StopsFactory StopsFactory { get; }
        Gradient Construct();
    }
}
