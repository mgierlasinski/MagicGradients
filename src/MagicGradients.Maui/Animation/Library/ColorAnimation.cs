namespace MagicGradients.Forms.Animation;

public class ColorTweener : ITweener<Color>
{
    public Color Tween(Color @from, Color to, double progress)
    {
        return Color.FromRgb(
            from.Red + (to.Red - from.Red) * progress,
            from.Green + (to.Green - from.Green) * progress,
            from.Blue + (to.Blue - from.Blue) * progress);
    }
}

public class ColorAnimation : PropertyAnimation<Color>
{
    public override ITweener<Color> Tweener { get; } = new ColorTweener();
}

public class ColorAnimationUsingKeyFrames : PropertyAnimationUsingKeyFrames<Color>
{
    public override ITweener<Color> Tweener { get; } = new ColorTweener();
}

public class ColorKeyFrame : KeyFrame<Color> { }
