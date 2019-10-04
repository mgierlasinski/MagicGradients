using SkiaSharp;
using System.Collections.Generic;
using Xamarin.Forms;

namespace MagicGradients
{
    [ContentProperty(nameof(Stops))]
    public abstract class Gradient : BindableObject
    {
        public IList<GradientStop> Stops { get; set; } = new List<GradientStop>();

        public abstract SKShader CreateShader(SKPaint paint, SKImageInfo info);
    }
}
