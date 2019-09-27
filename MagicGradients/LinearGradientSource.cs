using System.Collections.Generic;
using Xamarin.Forms;

namespace MagicGradients
{
    [ContentProperty(nameof(Gradients))]
    public class LinearGradientSource : BindableObject, ILinearGradientSource
    {
        public IList<LinearGradient> Gradients { get; set; } = new List<LinearGradient>();

        public IEnumerable<LinearGradient> GetGradients() => Gradients;
    }
}
