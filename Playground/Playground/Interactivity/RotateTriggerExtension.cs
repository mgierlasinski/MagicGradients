using System;
using MagicGradients.Toolkit.Controls;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Playground.Interactivity
{
    public class RotateTriggerExtension : IMarkupExtension<TriggerBase>
    {
        public uint Duration { get; set; }
        public double Forward { get; set; }
        public double Backward { get; set; }

        public BindingBase IsRunning { get; set; }

        public TriggerBase ProvideValue(IServiceProvider serviceProvider)
        {
            var trigger = new DataTrigger(typeof(VisualElement))
            {
                Binding = IsRunning,
                Value = true
            };

            trigger.EnterActions.Add(new RotateTriggerAction { Duration = Duration, RotateTo = Forward });
            trigger.ExitActions.Add(new RotateTriggerAction { Duration = Duration, RotateTo = Backward });

            return trigger;
        }

        object IMarkupExtension.ProvideValue(IServiceProvider serviceProvider)
        {
            return ProvideValue(serviceProvider);
        }
    }
}
