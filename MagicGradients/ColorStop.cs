using Xamarin.Forms;

namespace MagicGradients
{
    public class ColorStop
    {
        public float Offset { get; set; }

        public Color Color { get; set; }

        public override string ToString()
        {
            return $"Offset={Offset}, Color=[{Color.ToString()}]";
        }
    }
}