using Xamarin.Forms;

namespace Playground.Models
{
    public class GradientTheme
    {
        public Color Color { get; set; }

        public string Tag { get; set; }

        public GradientTheme(Color color, string tag)
        {
            Color = color;
            Tag = tag;
        }
    }
}
