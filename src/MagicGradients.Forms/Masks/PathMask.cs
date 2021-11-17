using Xamarin.Forms;

namespace MagicGradients.Masks
{
    public class PathMask : GradientMask, IPathMask
    {
        public static readonly BindableProperty DataProperty = BindableProperty.Create(nameof(Data),
            typeof(string), typeof(PathMask));

        public string Data
        {
            get => (string)GetValue(DataProperty);
            set => SetValue(DataProperty, value);
        }
    }
}
