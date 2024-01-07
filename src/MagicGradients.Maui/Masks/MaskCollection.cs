using MagicGradients.Masks;

namespace MagicGradients.Maui.Masks;

[ContentProperty(nameof(Masks))]
public class MaskCollection : GradientMask, IMaskCollection
{
    private GradientElements<GradientMask> _masks;
    public GradientElements<GradientMask> Masks
    {
        get => _masks;
        set
        {
            _masks?.Release();
            _masks = value;
            _masks.AttachTo(this);
        }
    }

    public MaskCollection()
    {
        Masks = new GradientElements<GradientMask>();
    }

    protected override void OnBindingContextChanged()
    {
        base.OnBindingContextChanged();
        Masks.SetInheritedBindingContext(BindingContext);
    }

    public IReadOnlyList<IGradientMask> GetMasks() => Masks;
}
