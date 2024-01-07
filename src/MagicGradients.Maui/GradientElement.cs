namespace MagicGradients.Maui;

public abstract class GradientElement : BindableObject, IGradientVisualElement
{
    public IGradientVisualElement Parent { get; set; }

    public void InvalidateCanvas()
    {
        Parent?.InvalidateCanvas();
    }

    protected override void OnPropertyChanged(string propertyName = null)
    {
        base.OnPropertyChanged(propertyName);
        InvalidateCanvas();
    }
}
