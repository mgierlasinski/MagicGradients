using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;

namespace MagicGradients
{
    public class LinearGradientBuilder
    {
        private readonly List<LinearGradient> _gradients = new List<LinearGradient>();
        private LinearGradient _lastGradient;

        public LinearGradientBuilder AddGradient(double angle)
        {
            _lastGradient = new LinearGradient
            {
                Angle = angle,
                Stops = new List<ColorStop>()
            };

            _gradients.Add(_lastGradient);

            return this;
        }

        public LinearGradientBuilder AddStop(Color color, float? offset = null)
        {
            if (_lastGradient == null)
            {
                AddGradient(0);
            }

            var stop = new ColorStop
            {
                Color = color,
                Offset = offset ?? -1
            };

            _lastGradient.Stops.Add(stop);

            return this;
        }

        public LinearGradient[] Build()
        {
            foreach (var gradient in _gradients)
            {
                SetupUndefinedOffsets(gradient);
            }
            return _gradients.ToArray();
        }

        private void SetupUndefinedOffsets(LinearGradient gradient)
        {
            var undefinedStops = gradient.Stops.Where(x => x.Offset < 0).ToArray();

            if (undefinedStops.Length == 1)
            {
                undefinedStops[0].Offset = 0;
            }
            else if (undefinedStops.Length > 1)
            {
                var step = 1f / (undefinedStops.Length - 1);
                var currentOffset = 0f;

                foreach (var stop in undefinedStops)
                {
                    stop.Offset = currentOffset;
                    currentOffset += step;
                }
            }
        }
    }
}
