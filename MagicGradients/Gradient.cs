using MagicGradients.Renderers;
using System;
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

        public virtual void Measure()
        {
            var fromIndex = 0;

            for (var i = 0; i < Stops.Count; i++)
            {
                if (Stops[i].Offset >= 0 || i == Stops.Count - 1)
                {
                    SetupUndefinedOffsets(fromIndex, i);
                    fromIndex = i;
                }
            }
        }

        private void SetupUndefinedOffsets(int fromIndex, int toIndex)
        {
            var currentOffset = Math.Max(Stops[fromIndex].Offset, 0);
            var endOffset = Math.Abs(Stops[toIndex].Offset);

            var step = (endOffset - currentOffset) / (toIndex - fromIndex);

            for (var i = fromIndex; i <= toIndex; i++)
            {
                var stop = Stops[i];

                if (stop.Offset < 0)
                {
                    stop.Offset = currentOffset;
                }
                currentOffset += step;
            }
        }
    }
}
