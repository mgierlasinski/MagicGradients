using System.Threading.Tasks;
using Xamarin.Forms;

namespace MagicGradients.Animation
{
    public class Animate : Behavior<VisualElement>
    {
        private static VisualElement _associatedObject;

        public Timeline Animation { get; set; }

        protected override async void OnAttachedTo(VisualElement bindable)
        {
            base.OnAttachedTo(bindable);
            _associatedObject = bindable;

            if (Animation == null)
                return;

            if (Animation.Target == null)
                Animation.Target = _associatedObject;

            // TODO: is it required?
            await Task.Delay(250);
            await Animation.Begin(bindable);
        }

        protected override void OnDetachingFrom(VisualElement bindable)
        {
            Animation?.End();
            _associatedObject = null;
            base.OnDetachingFrom(bindable);
        }
    }
}
