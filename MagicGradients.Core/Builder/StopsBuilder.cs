using Microsoft.Maui.Graphics;
using System.Collections.Generic;

namespace MagicGradients.Builder
{
    public abstract class StopsBuilder<T>
    {
        protected abstract T Instance { get; }

        public IGradientFactory Factory { get; set; }
        public virtual List<IGradientStop> Stops { get; } = new();
        
        public T AddStop(Color color, Offset? offset = null)
        {
            Stops.Add(Factory.CreateStop(color, offset));
            return Instance;
        }

        public T AddStops(Color color, IEnumerable<Offset> offsets)
        {
            foreach (var offset in offsets)
            {
                Stops.Add(Factory.CreateStop(color, offset));
            }
            return Instance;
        }

        public T AddStops(params Color[] colors)
        {
            foreach (var color in colors)
            {
                Stops.Add(Factory.CreateStop(color));
            }
            return Instance;
        }
    }
}
