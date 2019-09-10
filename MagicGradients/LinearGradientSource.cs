using System.Collections.Generic;
using Xamarin.Forms;

namespace MagicGradients
{
    [ContentProperty(nameof(Gradients))]
    public class LinearGradientSource : BindableObject, ILinearGradientSource
    {
        public static readonly BindableProperty GradientsProperty = BindableProperty.Create(
            nameof(Gradients), typeof(List<LinearGradient>), typeof(LinearGradientSource), new List<LinearGradient>());

        public List<LinearGradient> Gradients
        {
            get => (List<LinearGradient>)GetValue(GradientsProperty);
            set => SetValue(GradientsProperty, value);
        }

        public IEnumerable<LinearGradient> GetGradients() => Gradients;
    }
}
