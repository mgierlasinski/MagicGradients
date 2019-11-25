using Xamarin.Forms;

namespace MagicGradients
{
    public class GradientStop : BindableObject
    {
        public static readonly BindableProperty ColorProperty = BindableProperty.Create(
            nameof(Color), typeof(Color), typeof(GradientStop), Color.White);

        public Color Color
        {
            get => (Color)GetValue(ColorProperty);
            set => SetValue(ColorProperty, value);
        }

        public static readonly BindableProperty OffsetProperty = BindableProperty.Create(
            nameof(Offset), typeof(float), typeof(GradientStop), -1f);

        public float Offset
        {
            get => (float)GetValue(OffsetProperty);
            set => SetValue(OffsetProperty, value);
        }

        public override string ToString()
        {
            return $"Offset={Offset}, Color=[{Color.ToString()}]";
        }
    }
}