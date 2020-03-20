using System.Collections;
using Xamarin.Forms;

namespace MagicGradients
{
    public abstract class GradientElement : BindableObject, IGradientVisualElement
    {
        public IGradientVisualElement Parent { get; set; }

        public void InvalidateCanvas()
        {
            Parent?.InvalidateCanvas();
        }

        protected override void OnPropertyChanged(string propertyName = null)
        {
            InvalidateCanvas();
        }

        protected void SetParent(IList collection, IGradientVisualElement parent)
        {
            for (var i = 0; i < collection.Count; i++)
            {
                var item = (GradientElement)collection[i];
                item.Parent = parent;
            }
        }
    }
}
