using System.Collections.Generic;
using Xamarin.Forms;

namespace MagicGradients
{
    [ContentProperty(nameof(Stops))]
    public class LinearGradient : BindableObject
    {
        public IList<ColorStop> Stops { get; set; } = new List<ColorStop>();

        public double Angle { get; set; }

        public bool IsRepeating { get; set; }

        public override string ToString()
        {
            return $"Angle={Angle}, Stops=LinearGradientStop[{Stops.Count}]";
        }
    }
}
