using MagicGradients.Masks;
using Xamarin.Forms;

namespace Playground.Features.Editor.Handlers
{
    public class MaskTemplateSelector : DataTemplateSelector
    {
        public DataTemplate RectangleTemplate { get; set; }
        public DataTemplate EllipseTemplate { get; set; }
        public DataTemplate TextTemplate { get; set; }
        public DataTemplate PathTemplate { get; set; }

        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            if (item is EllipseMask)
                return EllipseTemplate;

            if (item is RectangleMask)
                return RectangleTemplate;

            if (item is TextMask)
                return TextTemplate;

            if (item is PathMask)
                return PathTemplate;

            return null;
        }
    }
}
