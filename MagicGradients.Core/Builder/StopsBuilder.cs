using Microsoft.Maui.Graphics;
using System.Collections.Generic;

namespace MagicGradients.Builder
{
    public abstract class StopsBuilder<T>
    {
        public virtual StopsFactory StopsFactory { get; } = new StopsFactory();
        protected abstract T Instance { get; }

        public T AddStop(Color color, Offset? offset = null)
        {
            StopsFactory.CreateStop(color, offset);
            return Instance;
        }

        public T AddStops(Color color, IEnumerable<Offset> offsets)
        {
            foreach (var offset in offsets)
            {
                StopsFactory.CreateStop(color, offset);
            }
            return Instance;
        }

        public T AddStops(params Color[] colors)
        {
            foreach (var color in colors)
            {
                StopsFactory.CreateStop(color);
            }
            return Instance;
        }
    }
}
