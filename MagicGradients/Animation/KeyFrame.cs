namespace MagicGradients.Animation
{
    public class KeyFrame
    {
        public int KeyTime { get; set; }
        public EasingType Easing { get; set; } = EasingType.Linear;
    }

    public class KeyFrame<TValue> : KeyFrame
    {
        public TValue Value { get; set; }
    }
}
