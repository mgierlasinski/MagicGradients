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
                stop.RenderOffset = !stop.Offset.IsEmpty && stop.Offset.Type == OffsetType.Absolute 
                    ? (float)CalculateRenderOffset(stop.Offset.Value, width, height) 
                    : (float)stop.Offset.Value;
            }

            CalculateUndefinedOffsets();
        }

        private void CalculateUndefinedOffsets()
        {
            var fromIndex = 0;

            for (var i = 0; i < Stops.Count; i++)
            {
                if (Stops[i].RenderOffset >= 0 || i == Stops.Count - 1)
                {
                    CalculateUndefinedRange(fromIndex, i);
                    fromIndex = i;
                }
            }
        }

        private void CalculateUndefinedRange(int fromIndex, int toIndex)
        {
            var currentOffset = Math.Max(Stops[fromIndex].RenderOffset, 0);
            var endOffset = Math.Abs(Stops[toIndex].RenderOffset);

            var step = (endOffset - currentOffset) / (toIndex - fromIndex);

            for (var i = fromIndex; i <= toIndex; i++)
            {
                var stop = Stops[i];

                if (stop.RenderOffset < 0)
                {
                    stop.RenderOffset = currentOffset;
                }
                currentOffset += step;
            }
        }
    }
}
