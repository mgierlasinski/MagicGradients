using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;

namespace MagicGradients
{
    public class LinearGradientBuilder
    {
        private readonly List<LinearGradient> _gradients = new List<LinearGradient>();
        private LinearGradient _lastGradient;

        public LinearGradientBuilder AddGradient(int angle)
        {
            _lastGradient = new LinearGradient
            {
                Angle = angle,
                Stops = new List<LinearGradientStop>()
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

            var stop = new LinearGradientStop { Color = color };

            if (offset == null)
            {
                stop.Offset = _lastGradient.Stops.Any() ? 1 : 0;
            }
            else
            {
                stop.Offset = offset.Value;
            }

            _lastGradient.Stops.Add(stop);

            return this;
        }

        public LinearGradient[] Build() => _gradients.ToArray();

        public ILinearGradientSource ToGradientSource() => new LinearGradientSource
        {
            Gradients = _gradients.ToList()
        };
    }
}
