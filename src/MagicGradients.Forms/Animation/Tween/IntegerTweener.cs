namespace MagicGradients.Forms.Animation
{
    public class IntegerTweener : ITweener<int>
    {
        public int Tween(int @from, int to, double progress)
        {
            return (int)(from + (to - from) * progress);
        }
    }
}
