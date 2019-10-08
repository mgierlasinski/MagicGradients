using System.Collections.Generic;
using System.Linq;
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

        public GradientBuilder AddRadialGradient()
        {
            _lastGradient = new RadialGradient
            {
                Center = new Point(0.5, 0.5),
                RadiusX = 1, 
                RadiusY = 1,
                Flags = RadialGradientFlags.All,
                Stops = new List<GradientStop>()
            };

            _gradients.Add(_lastGradient);

            return this;
        }

        public GradientBuilder AddRadialGradient(Point center, RadialGradientShape shape, RadialGradientShapeSize size)
        {
            _lastGradient = new RadialGradient
            {
                Center = center,
                Shape = shape,
                ShapeSize = size,
                RadiusX = 1,
                RadiusY = 1,
                Flags = RadialGradientFlags.All,
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
