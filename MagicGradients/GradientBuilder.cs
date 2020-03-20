using System.Collections.Generic;
using System.Collections.ObjectModel;
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
                Stops = new ObservableCollection<GradientStop>()
            };

            _gradients.Add(_lastGradient);

            return this;
        }

        public GradientBuilder AddRadialGradient(Point center, RadialGradientShape shape, RadialGradientSize size, 
            RadialGradientFlags flags = RadialGradientFlags.PositionProportional, bool isRepeating = false)
        {
            _lastGradient = new RadialGradient
            {
                Center = center,
                Shape = shape,
                Size = size,
                Flags = flags,
                IsRepeating = isRepeating,
                Stops = new ObservableCollection<GradientStop>()
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
            return _gradients.ToArray();
        }
    }
}
