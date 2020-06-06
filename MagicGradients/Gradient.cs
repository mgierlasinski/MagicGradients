using MagicGradients.Renderers;
using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace MagicGradients
{
    [ContentProperty(nameof(Stops))]
    public abstract class Gradient : GradientElement, IGradientSource
    {
        private GradientElements<GradientStop> _stops;
        public GradientElements<GradientStop> Stops
        {
            get => _stops;
            set
            {
                _stops?.Release();
                _stops = value;
                _stops.AttachTo(this);
            }
        }

        public static readonly BindableProperty IsRepeatingProperty = BindableProperty.Create(
            nameof(IsRepeating), typeof(bool), typeof(LinearGradient), false);

        public bool IsRepeating
        {
            get => (bool)GetValue(IsRepeatingProperty);
            set => SetValue(IsRepeatingProperty, value);
        }

        protected Gradient()
        {
            Stops = new GradientElements<GradientStop>();
        }

        public IEnumerable<Gradient> GetGradients() => new[] { this };

        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();
            Stops.SetInheritedBindingContext(BindingContext);
        }

        public abstract void Render(RenderContext context);
        protected abstract double CalculateRenderOffset(double offset, int width, int height);

        public virtual void Measure(int width, int height)
        {
            foreach (var stop in Stops)
            {
                if (stop.Offset.IsEmpty)
                    continue;

                stop.RenderOffset = stop.Offset.Type == OffsetType.Proportional
                    ? (float)stop.Offset.Value
                    : (float)CalculateRenderOffset(stop.Offset.Value, width, height);
            }

            CalculateUndefinedOffsets();
        }

        private void CalculateUndefinedOffsets()
        {
            var fromIndex = 0;

            for (var i = 0; i < Stops.Count; i++)
            {
                if (Stops[i].Offset.Value >= 0 || i == Stops.Count - 1)
                {
                    CalculateUndefinedRange(fromIndex, i);
                    fromIndex = i;
                }
            }
        }

        private void CalculateUndefinedRange(int fromIndex, int toIndex)
        {
            var currentOffset = Math.Max(Stops[fromIndex].Offset.Value, 0);
            var endOffset = Math.Abs(Stops[toIndex].Offset.Value);

            var step = (endOffset - currentOffset) / (toIndex - fromIndex);

            for (var i = fromIndex; i <= toIndex; i++)
            {
                var stop = Stops[i];

                if (stop.Offset.Value < 0)
                {
                    stop.RenderOffset = (float)currentOffset;
                }
                currentOffset += step;
            }
        }
    }
}
