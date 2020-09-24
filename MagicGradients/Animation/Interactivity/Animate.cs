using System;
using Xamarin.Forms;

namespace MagicGradients.Animation
{
    [ContentProperty(nameof(Animation))]
    public class Animate : Behavior<VisualElement>
    {
        private static VisualElement _associatedObject;

        public Timeline Animation { get; set; }

        protected override void OnAttachedTo(VisualElement bindable)
        {
            base.OnAttachedTo(bindable);
            _associatedObject = bindable;

            if (Animation == null)
                return;

            if (Animation.Target == null)
                Animation.Target = _associatedObject;

            _associatedObject.SizeChanged += OnAnimatorLoaded;
        }

        private void OnAnimatorLoaded(object sender, EventArgs e)
        {
            var animator = (VisualElement)sender;
            animator.SizeChanged -= OnAnimatorLoaded;
            Animation?.Begin(animator);
        }

        protected override void OnDetachingFrom(VisualElement bindable)
        {
            Animation?.End();
            _associatedObject = null;
            base.OnDetachingFrom(bindable);
        }
    }
}
