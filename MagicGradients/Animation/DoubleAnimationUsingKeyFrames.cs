namespace MagicGradients.Animation
{
    public class DoubleAnimationUsingKeyFrames : PropertyAnimationUsingKeyFrames<double>
    {
        protected override double GetProgressValue(double @from, double to, double progress)
        {
            return AnimationHelper.GetDoubleValue(@from, to, progress);
        }
    }
}
