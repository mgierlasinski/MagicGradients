using System.Diagnostics;
using MauiAnimation = Microsoft.Maui.Controls.Animation;

namespace MagicGradients.Forms.Animation;

public abstract class Timeline : BindableObject
{
    private readonly string _handle = Guid.NewGuid().ToString();
    private int _playCount;

    public uint Duration { get; set; } = 0;
    public int Delay { get; set; } = 0;
    public RepeatBehavior RepeatBehavior { get; set; } = new RepeatBehavior(RepeatBehaviorType.Count, 1);
    public bool AutoReverse { get; set; }
    public Easing Easing { get; set; } = Easing.Linear;
    public BindableObject Target { get; set; } = default;
    public VisualElement Animator { get; private set; } = default;
    public bool IsRunning { get; private set; }

    public async Task Begin(VisualElement animator)
    {
        Animator = animator;
        OnBegin();
        IsRunning = true;

        if (Delay > 0)
        {
            await Task.Delay(Delay);
        }

        Animate();
    }

    private void Animate()
    {
        Animator.Animate(_handle, OnAnimate(),
            length: Duration,
            easing: Easing,
            repeat: IsRepeat,
            finished: (v, c) =>
            {
                var repeat = IsRepeat();
                IsRunning = !repeat;
                Debug.WriteLine($"Timeline Finished (plays: {_playCount + 1}, repeat: {repeat}, handle: {_handle})");
                _playCount++;
                OnFinished();
            });
    }

    public void End()
    {
        Animator?.AbortAnimation(_handle);
        IsRunning = false;
        _playCount = 0;
    }

    public virtual void OnBegin()
    {
        if (Target == null)
        {
            throw new NullReferenceException("Null Target property.");
        }
    }

    public abstract MauiAnimation OnAnimate();
    public virtual void OnFinished() { }

    protected bool IsRepeat()
    {
        if (RepeatBehavior.Type == RepeatBehaviorType.Forever)
            return true;

        return _playCount < RepeatBehavior.Count - 1;
    }
}
