using System.Collections.Generic;
using Xamarin.Forms;

namespace MagicGradients
{
    [ContentProperty(nameof(Stops))]
    public class LinearGradient : BindableObject
    {
        public static readonly BindableProperty StopsProperty = BindableProperty.Create(
            nameof(Stops), typeof(List<LinearGradientStop>), typeof(LinearGradient), new List<LinearGradientStop>());

        public List<LinearGradientStop> Stops
        {
            get => (List<LinearGradientStop>)GetValue(StopsProperty);
            set => SetValue(StopsProperty, value);
        }

        public double Angle { get; set; }

        public override string ToString()
        {
            return $"Angle={Angle}, Stops=LinearGradientStop[{Stops.Count}]";
        }
    }
}
