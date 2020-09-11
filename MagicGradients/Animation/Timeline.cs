using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MagicGradients.Animation
{
    public abstract class Timeline : BindableObject
    {
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

            BeginAnimation();
        }

        public void End()
        {
            ViewExtensions.CancelAnimations(Animator);
        }

        protected virtual void BeginAnimation()
        {
            //var taskCompletionSource = new TaskCompletionSource<bool>();

            Animator.Animate(Guid.NewGuid().ToString(), OnAnimate(),
                length: Duration,
                easing: Easing.ToEasing(),
                finished: (v, c) =>
                {
                    Debug.WriteLine("Finished Timeline");
                    _playCount++;
                    OnFinished();
                    //if (IsRepeat())
                    //    OnRepeat();
                    //else
                    //    taskCompletionSource.SetResult(c);
                },
                repeat: IsRepeat);

            //return taskCompletionSource.Task;
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
