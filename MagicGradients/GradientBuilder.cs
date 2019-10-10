using System.Collections.Generic;
using Xamarin.Forms;

namespace MagicGradients
{
    public class GradientBuilder
    {
        private readonly List<Gradient> _gradients = new List<Gradient>();
        private Gradient _lastGradient;

        public GradientBuilder AddLinearGradient(double angle, bool isRepeating = false)
        {
            _lastGradient = new LinearGradient
            {
                Angle = angle,
                IsRepeating = isRepeating,
                Stops = new List<GradientStop>()
            };

            _gradients.Add(_lastGradient);

            return this;
        }

        public GradientBuilder AddRadialGradient(Point center, RadialGradientFlags flags, RadialGradientShape shape, RadialGradientSize size)
        {
            _lastGradient = new RadialGradient
            {
                Center = center,
                Flags = flags,
                Shape = shape,
                Size = size,
                Stops = new List<GradientStop>()
            };

            _gradients.Add(_lastGradient);

            return this;
        }

        public GradientBuilder AddStop(Color color, float? offset = null)
        {
            if (_lastGradient == null)
            {
                AddLinearGradient(0);
            }

            var stop = new GradientStop
            {
                Color = color,
                Offset = offset ?? -1
            };

            _lastGradient.Stops.Add(stop);

            return this;
        }

        public GradientBuilder AddStops(Color color, IEnumerable<float> offsets)
        {
            foreach (var offset in offsets)
            {
                AddStop(color, offset);
            }

            return this;
        }

        public Gradient[] Build()
        {
            foreach (var gradient in _gradients)
            {
                SetupUndefinedOffsets(gradient);
            }
            return _gradients.ToArray();
        }

        private void SetupUndefinedOffsets(Gradient gradient)
        {
            var step = 1f / (gradient.Stops.Count - 1);
            var currentOffset = 0f;

            foreach (var stop in gradient.Stops)
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
