using System.Collections.Generic;
using Xamarin.Forms;

namespace MagicGradients
{
    [ContentProperty(nameof(Gradients))]
    public class GradientCollection : BindableObject, IGradientSource
    {
        public IList<Gradient> Gradients { get; set; } = new List<Gradient>();

        public IEnumerable<Gradient> GetGradients() => Gradients;
    }
}
