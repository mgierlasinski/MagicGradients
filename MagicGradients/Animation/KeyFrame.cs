using Xamarin.Forms;

namespace MagicGradients.Animation
{
    public class KeyFrame<TValue>
    {
        public int KeyTime { get; set; }
        public TValue Value { get; set; }
        public EasingType Easing { get; set; }
    }

    public class DoubleKeyFrame : KeyFrame<double>
    {
        
    }

    public class ColorKeyFrame : KeyFrame<Color>
    {

    }

    public class PointKeyFrame : KeyFrame<Point>
    {

    }
}
