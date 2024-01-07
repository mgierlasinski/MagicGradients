namespace MagicGradients.Maui;

public class LinearGradient : Gradient, ILinearGradient
{
    public static readonly BindableProperty AngleProperty = BindableProperty.Create(
        nameof(Angle), typeof(double), typeof(LinearGradient), 0d);
    
    public double Angle
    {
        get => (double)GetValue(AngleProperty);
        set => SetValue(AngleProperty, value);
    }
}
