using MagicGradients.Renderers;
using System.Collections.Generic;
using Xamarin.Forms;

namespace MagicGradients
{
    [ContentProperty(nameof(Stops))]
    public abstract class Gradient : BindableObject, IGradientSource
    {
        public IList<GradientStop> Stops { get; set; } = new List<GradientStop>();

        public abstract void Render(RenderContext context);

        public IEnumerable<Gradient> GetGradients()
        {
            return new List<Gradient> { this };
        }
    }
}
