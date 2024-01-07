namespace MagicGradients.Maui;

[ContentProperty(nameof(Stops))]
public abstract class Gradient : GradientElement, IGradient, IGradientSource
{
    private GradientElements<GradientStop> _stops;
    public GradientElements<GradientStop> Stops
    {
        get => _stops;
        set
        {
            _stops?.Release();
            _stops = value;
            _stops.AttachTo(this);
        }
    }

    public static readonly BindableProperty IsRepeatingProperty = BindableProperty.Create(
        nameof(IsRepeating), 
        typeof(bool), 
        typeof(LinearGradient), 
        false);

    public bool IsRepeating
    {
        get => (bool)GetValue(IsRepeatingProperty);
        set => SetValue(IsRepeatingProperty, value);
    }

    protected Gradient()
    {
        Stops = new GradientElements<GradientStop>();
    }

    public IReadOnlyList<IGradientStop> GetStops() => Stops;
    public IReadOnlyList<IGradient> GetGradients() => new[] { this };

    protected override void OnBindingContextChanged()
    {
        base.OnBindingContextChanged();
        Stops.SetInheritedBindingContext(BindingContext);
    }
}