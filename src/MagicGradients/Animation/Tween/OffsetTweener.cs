namespace MagicGradients.Animation.Tween
{
    public class OffsetTweener : ITweener<Offset>
    {
        public Offset Tween(Offset @from, Offset to, double progress)
        {
            return new Offset(from.Value + (to.Value - from.Value) * progress, from.Type);
        }
    }
}
