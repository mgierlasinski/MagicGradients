namespace MagicGradients.Forms.Animation;

public class DoubleTweener : ITweener<double>
{
    public double Tween(double @from, double to, double progress)
    {
        return from + (to - from) * progress;
    }
}

public class DoubleAnimation : PropertyAnimation<double>
{
    public override ITweener<double> Tweener { get; } = new DoubleTweener();
}

public class DoubleAnimationUsingKeyFrames : PropertyAnimationUsingKeyFrames<double>
{
    public override ITweener<double> Tweener { get; } = new DoubleTweener();
}

public class DoubleKeyFrame : KeyFrame<double> { }
