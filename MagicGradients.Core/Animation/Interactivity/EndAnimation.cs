using Xamarin.Forms;

namespace MagicGradients.Animation
{
    public class EndAnimation : TriggerAction<VisualElement>
    {
        public Timeline Animation { get; set; }

        protected override void Invoke(VisualElement sender)
        {
            Animation?.End();
        }
    }
}
