using Xamarin.Forms;

namespace MagicGradients
{
    public class GradientStop : GradientElement
    {
        public static readonly BindableProperty ColorProperty = BindableProperty.Create(
            nameof(Color), typeof(Color), typeof(GradientStop), Color.White);

        public Color Color
        {
            get => (Color)GetValue(ColorProperty);
            set => SetValue(ColorProperty, value);
        }

        public static readonly BindableProperty OffsetProperty = BindableProperty.Create(
            nameof(Offset), typeof(float), typeof(GradientStop), -1f, 
            propertyChanged: OnOffsetChanged);

        public float Offset
        {
            get => (float)GetValue(OffsetProperty);
            set => SetValue(OffsetProperty, value);
        }

        public float RenderOffset { get; set; } = -1f;

        private static void OnOffsetChanged(BindableObject bindable, object oldvalue, object newvalue)
        {
            ((GradientStop)bindable).RenderOffset = (float)newvalue;
        }

        public override string ToString()
        {
            return $"Offset={Offset}, Color=[{Color.ToString()}]";
        }
    }
}