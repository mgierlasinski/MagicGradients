namespace MagicGradients.Maui;

public class GradientStop : GradientElement, IGradientStop
{
    public static readonly BindableProperty ColorProperty = BindableProperty.Create(
        nameof(Color), typeof(Color), typeof(GradientStop), Colors.White);

    public Color Color
    {
        get => (Color)GetValue(ColorProperty);
        set => SetValue(ColorProperty, value);
    }

    public static readonly BindableProperty OffsetProperty = BindableProperty.Create(
        nameof(Offset), typeof(Offset), typeof(GradientStop), Offset.Empty);

    public Offset Offset
    {
        get => (Offset)GetValue(OffsetProperty);
        set => SetValue(OffsetProperty, value);
    }

    public float RenderOffset { get; set; } = -1f;

    public override string ToString()
    {
        return $"Offset={Offset}, Color=[{Color}]";
    }
}