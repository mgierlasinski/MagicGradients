using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;

namespace MagicGradients.Animation
{
    public abstract class PropertyAnimationUsingKeyFrames<TValue> : Timeline
    {
        public BindableProperty TargetProperty { get; set; } = default;
        public List<KeyFrame<TValue>> KeyFrames { get; set; } = new List<KeyFrame<TValue>>();

        private List<KeyFrame<TValue>> _sortedKeyFrames;

        public override void OnBegin()
        {
            base.OnBegin();

            if (TargetProperty == null)
            {
                throw new NullReferenceException("Null Target property.");
            }
        }

        public override Xamarin.Forms.Animation OnAnimate()
        {
            var initialKeyFrame = new KeyFrame<TValue>
            {
                Value = (TValue)Target.GetValue(TargetProperty),
                KeyTime = 0
            };

            _sortedKeyFrames = KeyFrames.OrderBy(x => x.KeyTime).ToList();
            _sortedKeyFrames.Insert(0, initialKeyFrame);

            var animation = new Xamarin.Forms.Animation();

            for (var i = 1; i < _sortedKeyFrames.Count; i++)
            {
                var fromFrame = _sortedKeyFrames[i - 1];
                var toFrame = _sortedKeyFrames[i];

                var frameAnimation = new Xamarin.Forms.Animation(x =>
                {
                    var value = GetProgressValue(fromFrame.Value, toFrame.Value, x);
                    Target.SetValue(TargetProperty, value);
                },
                easing: Easing.ToEasing());

                var beginAt = fromFrame.KeyTime / Duration;
                var endAt = toFrame.KeyTime / Duration;

                animation.Add(beginAt, endAt, frameAnimation);
            }

            return animation;
        }

        protected abstract TValue GetProgressValue(TValue from, TValue to, double progress);
    }
}
