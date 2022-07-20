using MauiAnimation = Microsoft.Maui.Controls.Animation;

namespace MagicGradients.Forms.Animation;

[ContentProperty(nameof(KeyFrames))]
public abstract class PropertyAnimationUsingKeyFrames<TValue> : Timeline
{
    private KeyFrame _initialKeyFrame;
    private List<KeyFrame> _sortedKeyFrames;

    public BindableProperty TargetProperty { get; set; } = default;
    public List<KeyFrame> KeyFrames { get; set; } = new List<KeyFrame>();
    public abstract ITweener<TValue> Tweener { get; }

    public override void OnBegin()
    {
        base.OnBegin();

        if (TargetProperty == null)
        {
            throw new NullReferenceException("Null Target property.");
        }

        if (!KeyFrames.Any())
        {
            throw new ArgumentException("No key frames");
        }

        InitFrames();
    }

    private void InitFrames()
    {
        if (_initialKeyFrame == null)
        {
            _initialKeyFrame = new KeyFrame<TValue>
            {
                Value = (TValue)Target.GetValue(TargetProperty),
                KeyTime = 0
            };
        }

        _sortedKeyFrames = KeyFrames.OrderBy(x => x.KeyTime).ToList();
        _sortedKeyFrames.Insert(0, _initialKeyFrame);

        Duration = (uint)_sortedKeyFrames.Last().KeyTime;
    }

    public override MauiAnimation OnAnimate()
    {
        var animation = new MauiAnimation();

        for (var i = 1; i < _sortedKeyFrames.Count; i++)
        {
            var fromFrame = (KeyFrame<TValue>)_sortedKeyFrames[i - 1];
            var toFrame = (KeyFrame<TValue>)_sortedKeyFrames[i];

            var toFrameEasing = toFrame.Easing != Easing.Linear
                ? toFrame.Easing
                : Easing;

            var frameAnimation = new MauiAnimation(x =>
            {
                var value = Tweener.Tween(fromFrame.Value, toFrame.Value, x);
                Target.SetValue(TargetProperty, value);
            },
            easing: toFrameEasing);

            var beginAt = (double)fromFrame.KeyTime / Duration;
            var endAt = (double)toFrame.KeyTime / Duration;

            animation.Add(beginAt, endAt, frameAnimation);
        }

        return animation;
    }
}
