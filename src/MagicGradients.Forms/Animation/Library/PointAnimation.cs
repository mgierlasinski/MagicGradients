using Xamarin.Forms;

namespace MagicGradients.Forms.Animation
{
    public class PointTweener : ITweener<Point>
    {
        public Point Tween(Point @from, Point to, double progress)
        {
            return new Point(
                from.X + (to.X - from.X) * progress,
                from.Y + (to.Y - from.Y) * progress);
        }
    }

    public class PointAnimation : PropertyAnimation<Point>
    {
        public override ITweener<Point> Tweener { get; } = new PointTweener();
    }

    public class PointAnimationUsingKeyFrames : PropertyAnimationUsingKeyFrames<Point>
    {
        public override ITweener<Point> Tweener { get; } = new PointTweener();
    }

    public class PointKeyFrame : KeyFrame<Point> { }
}
