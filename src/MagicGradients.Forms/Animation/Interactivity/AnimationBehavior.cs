using System;
using Xamarin.Forms;

namespace MagicGradients.Animation
{
    [ContentProperty(nameof(Animation))]
    public class AnimationBehavior : Behavior<VisualElement>
    {
        private VisualElement _associatedObject;

        public Timeline Animation { get; set; }

        protected override void OnAttachedTo(VisualElement bindable)
        {
            base.OnAttachedTo(bindable);
            _associatedObject = bindable;

            if (Animation == null)
                return;

            Animation.Target ??= _associatedObject;
            _associatedObject.SizeChanged += OnAnimatorLoaded;
        }

        private void OnAnimatorLoaded(object sender, EventArgs e)
        {
            var animator = (VisualElement)sender;
            animator.SizeChanged -= OnAnimatorLoaded;

            if (animator.TryFindParent<Page>(out var page))
            {
                page.Appearing += PageOnAppearing;
                page.Disappearing += PageOnDisappearing;
            }

            Animation?.Begin(animator);
        }

        private void PageOnAppearing(object sender, EventArgs e)
        {
            if (Animation?.IsRunning == false)
            {
                Animation?.Begin(_associatedObject);
            }
        }

        private void PageOnDisappearing(object sender, EventArgs e)
        {
            Animation?.End();
        }

        protected override void OnDetachingFrom(VisualElement bindable)
        {
            Animation?.End();
            _associatedObject = null;
            base.OnDetachingFrom(bindable);
        }
    }
}
