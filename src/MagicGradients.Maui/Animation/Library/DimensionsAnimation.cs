namespace MagicGradients.Forms.Animation;

public class DimensionsTweener : ITweener<Dimensions>
{
    public Dimensions Tween(Dimensions @from, Dimensions to, double progress)
    {
        var typeWidth = from.Width.Type;
        var typeHeight = from.Height.Type;

        return new Dimensions(
            new Offset(from.Width.Value + (to.Width.Value - from.Width.Value) * progress, typeWidth),
            new Offset(from.Height.Value + (to.Height.Value - from.Height.Value) * progress, typeHeight));
    }
}

public class DimensionsAnimation : PropertyAnimation<Dimensions>
{
    public override ITweener<Dimensions> Tweener { get; } = new DimensionsTweener();
}

public class DimensionsAnimationUsingKeyFrames : PropertyAnimationUsingKeyFrames<Dimensions>
{
    public override ITweener<Dimensions> Tweener { get; } = new DimensionsTweener();
}

public class DimensionsKeyFrame : KeyFrame<Dimensions> { }
