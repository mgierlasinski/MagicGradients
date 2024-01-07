namespace MagicGradients.Maui;

[ContentProperty(nameof(Gradients))]
public class GradientCollection : GradientElement, IGradientSource
{
    private GradientElements<Gradient> _gradients;
    public GradientElements<Gradient> Gradients
    {
        get => _gradients;
        set
        {
            _gradients?.Release();
            _gradients = value;
            _gradients.AttachTo(this);
        }
    }

    public GradientCollection()
    {
        Gradients = new GradientElements<Gradient>();
    }

    public IReadOnlyList<IGradient> GetGradients() => Gradients;

    protected override void OnBindingContextChanged()
    {
        base.OnBindingContextChanged();
        Gradients.SetInheritedBindingContext(BindingContext);
    }
}