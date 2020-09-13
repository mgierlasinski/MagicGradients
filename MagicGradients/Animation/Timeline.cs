using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MagicGradients.Animation
{
    public abstract class Timeline : BindableObject
    {
        private string _handle = Guid.NewGuid().ToString();

        private int _playCount = 0;

        public uint Duration { get; set; } = 0;
        public int Delay { get; set; } = 0;
        public RepeatBehavior RepeatBehavior { get; set; } = new RepeatBehavior(RepeatBehaviorType.Count, 1);
        public bool AutoReverse { get; set; }
        public EasingType Easing { get; set; } = EasingType.Linear;
        public BindableObject Target { get; set; } = default;
        public VisualElement Animator { get; private set; } = default;

        public async Task Begin(VisualElement animator)
        {
            Animator = animator;
            OnBegin();

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
                easing: Easing.ToEasing(),
                finished: (v, c) =>
                {
                    _playCount++;
                    Debug.WriteLine($"Finished Timeline (plays: {_playCount})");
                    OnFinished();
                },
                repeat: IsRepeat);
        }

        public void End()
        {
            Animator.AbortAnimation(_handle);
            _playCount = 0;
        }

        public virtual void OnBegin()
        {
            if (Target == null)
            {
                throw new NullReferenceException("Null Target property.");
            }
        }

        public abstract Xamarin.Forms.Animation OnAnimate();
        protected virtual void OnFinished() { }

        protected bool IsRepeat()
        {
            if (RepeatBehavior.Type == RepeatBehaviorType.Forever)
                return true;

            return _playCount < RepeatBehavior.Count - 1;
        }
    }
}
