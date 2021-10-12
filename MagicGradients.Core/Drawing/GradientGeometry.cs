using System;
using System.Collections.Generic;

namespace MagicGradients.Drawing
{
    public abstract class GradientGeometry<TGradient> where TGradient : Gradient
    {
        public void CalculateOffsets(TGradient gradient, int width, int height)
        {
            foreach (var stop in gradient.Stops)
            {
                stop.RenderOffset = !stop.Offset.IsEmpty && stop.Offset.Type == OffsetType.Absolute
                    ? (float)CalculateRenderOffset(gradient, stop.Offset.Value, width, height)
                    : (float)stop.Offset.Value;
            }

            CalculateUndefinedOffsets(gradient.Stops);
        }

        protected abstract double CalculateRenderOffset(TGradient gradient, double offset, int width, int height);

        private void CalculateUndefinedOffsets(IList<GradientStop> stops)
        {
            var fromIndex = 0;

            for (var i = 0; i < stops.Count; i++)
            {
                if (stops[i].RenderOffset >= 0 || i == stops.Count - 1)
                {
                    CalculateUndefinedRange(stops, fromIndex, i);
                    fromIndex = i;
                }
            }
        }

        private void CalculateUndefinedRange(IList<GradientStop> stops, int fromIndex, int toIndex)
        {
            var currentOffset = Math.Max(stops[fromIndex].RenderOffset, 0);
            var endOffset = Math.Abs(stops[toIndex].RenderOffset);

            var step = (endOffset - currentOffset) / (toIndex - fromIndex);

            for (var i = fromIndex; i <= toIndex; i++)
            {
                var stop = stops[i];

                if (stop.RenderOffset < 0)
                {
                    stop.RenderOffset = currentOffset;
                }
                currentOffset += step;
            }
        }
    }
}
