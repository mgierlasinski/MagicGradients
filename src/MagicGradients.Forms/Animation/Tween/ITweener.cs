namespace MagicGradients.Animation.Tween
{
    public interface ITweener<TValue>
    {
        TValue Tween(TValue from, TValue to, double progress);
    }
}
