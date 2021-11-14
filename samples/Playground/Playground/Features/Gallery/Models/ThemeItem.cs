using Xamarin.Forms;

namespace Playground.Features.Gallery.Models
{
    public class ThemeItem
    {
        public string ColorRaw { get; set; }
        public Color Color { get; set; }
        public string Tag { get; set; }

        public override string ToString()
        {
            return ColorRaw;
        }
    }
}
