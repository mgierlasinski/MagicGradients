using MagicGradients;
using Playground.Extensions;
using System.Linq;
using Xamarin.Forms;

namespace Playground.Models
{
    public class GradientItem
    {
        public int Id { get; set; }

        public IGradientSource Source { get; set; }

        public Dimensions Size { get; set; }

        public bool HasColors(Color[] colors)
        {
            foreach (var gradient in Source.GetGradients())
            {
                var hasTheme = gradient.Stops.Any(x => x.Color.IsCloseTo(colors, 0.3));
                if (hasTheme)
                {
                    return true;
                }
            }

            return false;
        }
    }
}
