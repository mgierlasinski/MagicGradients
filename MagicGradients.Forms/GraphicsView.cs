using Microsoft.Maui.Graphics;
using Xamarin.Forms;

namespace MagicGradients.Forms
{
    public class GraphicsView : View
    {
        public static readonly BindableProperty DrawableProperty = BindableProperty.Create(
            nameof(Drawable), 
            typeof(IDrawable), 
            typeof(GraphicsView));

        public IDrawable Drawable
        {
            get => (IDrawable) GetValue(DrawableProperty);
            set => SetValue(DrawableProperty, value);
        }
    }
}