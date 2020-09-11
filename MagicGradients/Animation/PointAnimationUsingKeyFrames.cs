using Xamarin.Forms;

namespace MagicGradients.Animation
{
    public class PointAnimationUsingKeyFrames : PropertyAnimationUsingKeyFrames<Point>
    {
        protected override Point GetProgressValue(Point @from, Point to, double progress)
        {
            return AnimationHelper.GetPointValue(@from, to, progress);
        }
    }
}
