using System.Collections.Generic;
using Xamarin.Forms;

namespace MagicGradients
{
    public class StopsFactory
    {
        public List<GradientStop> Stops { get; } = new List<GradientStop>();

        public void CreateStop(Color color, Offset? offset = null)
        {
            var stop = new GradientStop
            {
                Color = color,
                Offset = offset ?? Offset.Empty
            };

            Stops.Add(stop);
        }
    }
}
