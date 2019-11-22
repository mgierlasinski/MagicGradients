using MagicGradients.Renderers;
using System.Collections.Generic;
using Xamarin.Forms;

namespace MagicGradients
{
    [ContentProperty(nameof(Stops))]
    public abstract class Gradient : BindableObject, IGradientSource
    {
        public IList<GradientStop> Stops { get; set; } = new List<GradientStop>();

        public static readonly BindableProperty IsRepeatingProperty = BindableProperty.Create(
            nameof(IsRepeating), typeof(bool), typeof(LinearGradient), false);

        public bool IsRepeating
        {
            get => (bool)GetValue(IsRepeatingProperty);
            set => SetValue(IsRepeatingProperty, value);
        }

        public abstract void Render(RenderContext context);

        public IEnumerable<Gradient> GetGradients()
        {
            return new List<Gradient> { this };
        }

        public void SetupUndefinedOffsets()
        {
            var step = 1f / (Stops.Count - 1);
            var currentOffset = 0f;

            foreach (var stop in Stops)
            {
                if (stop.Offset < 0)
                {
                    stop.Offset = currentOffset;
                }
                currentOffset += step;
            }
        }
    }
}
