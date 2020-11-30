using Xamarin.Forms;

namespace Playground.Interactivity
{
    public class RotateTriggerAction : TriggerAction<VisualElement>
    {
        public uint Duration { get; set; }
        public double RotateTo { get; set; }

        protected override void Invoke(VisualElement sender)
        {
            sender.RotateTo(RotateTo, Duration);
        }
    }
}
