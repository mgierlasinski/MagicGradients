using Xamarin.Forms;

namespace MagicGradients.Animation
{
    public class BeginAnimation : TriggerAction<VisualElement>
    {
        public Timeline Animation { get; set; }

        protected override void Invoke(VisualElement sender)
        {
            if (Animation == null)
                return;

            if (Animation.Target == null)
                Animation.Target = sender;

            Animation.Begin(sender);
        }
    }
}
