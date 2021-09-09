namespace MagicGradients.Animation.Tween
{
    public class DoubleTweener : ITweener<double>
    {
        public double Tween(double @from, double to, double progress)
        {
            return from + (to - from) * progress;
        }
    }
}
