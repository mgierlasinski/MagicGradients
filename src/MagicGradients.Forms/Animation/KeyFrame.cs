using Xamarin.Forms;

namespace MagicGradients.Forms.Animation
{
    public class KeyFrame
    {
        public int KeyTime { get; set; }
        public Easing Easing { get; set; } = Easing.Linear;
    }

    public class KeyFrame<TValue> : KeyFrame
    {
        public TValue Value { get; set; }
    }
}
