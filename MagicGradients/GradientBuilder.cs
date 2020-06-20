using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;

namespace MagicGradients
{
    public class GradientBuilder
    {
        private readonly List<Gradient> _gradients = new List<Gradient>();
        private readonly List<GradientStop> _stops = new List<GradientStop>();

        public GradientBuilder AddLinearGradient(double angle, bool isRepeating = false)
        {
            AddCachedStopsToLast();

            var linearGradient = new LinearGradient
            {
                Angle = angle,
                IsRepeating = isRepeating
            };

            _gradients.Add(linearGradient);

            return this;
        }

        public GradientBuilder AddRadialGradient(
            Point center, 
            RadialGradientShape shape, 
            RadialGradientSize size, 
            RadialGradientFlags flags = RadialGradientFlags.PositionProportional, 
            bool isRepeating = false)
        {
            AddCachedStopsToLast();

            var radialGradient = new RadialGradient
            {
                Center = center,
                Shape = shape,
                Size = size,
                Flags = flags,
                IsRepeating = isRepeating
            };

            _gradients.Add(radialGradient);

            return this;
        }

        public GradientBuilder AddStop(Color color, Offset? offset = null)
        {
            var stop = new GradientStop
            {
                Color = color,
                Offset = offset ?? Offset.Empty
            };

            _stops.Add(stop);

            return this;
        }

        public GradientBuilder AddStop(Color color, double offset)
        {
            return AddStop(color, Offset.Prop(offset));
        }

        public GradientBuilder AddStops(Color color, IEnumerable<Offset> offsets)
        {
            foreach (var offset in offsets)
            {
                AddStop(color, offset);
            }

            return this;
        }

        public GradientBuilder AddStops(Color color, IEnumerable<double> offsets)
        {
            foreach (var offset in offsets)
            {
                AddStop(color, offset);
            }

            return this;
        }

        private void AddCachedStopsToLast()
        {
            if (!_stops.Any())
                return;

            var lastGradient = _gradients.LastOrDefault();
            if (lastGradient == null)
            {
                lastGradient = CreateDefaultGradient();
                _gradients.Add(lastGradient);
            }
            lastGradient.Stops = new GradientElements<GradientStop>(_stops);

            _stops.Clear();
        }

        private Gradient CreateDefaultGradient() => new LinearGradient
        {
            Angle = 0,
            IsRepeating = false
        };

        public Gradient[] Build()
        {
            AddCachedStopsToLast();
            return _gradients.ToArray();
        }
    }
}
