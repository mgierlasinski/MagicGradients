using System.Collections.Generic;
using Xamarin.Forms;

namespace MagicGradients
{
    [ContentProperty(nameof(Gradients))]
    public class GradientSource : BindableObject, IGradientSource
    {
        public IList<IGradient> Gradients { get; set; } = new List<IGradient>();

        public IEnumerable<IGradient> GetGradients() => Gradients;
    }
}
