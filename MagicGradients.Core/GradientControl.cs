using MagicGradients.Masks;
using Xamarin.Forms;

namespace MagicGradients
{
    public static class GradientControl
    {
        public static readonly BindableProperty GradientSourceProperty = BindableProperty.Create(
            nameof(IGradientControl.GradientSource), 
            typeof(IGradientSource),
            typeof(IGradientControl), 
            propertyChanged: OnGradientElementChanged);

        public static readonly BindableProperty GradientSizeProperty = BindableProperty.Create(
            nameof(IGradientControl.GradientSize), 
            typeof(Dimensions), 
            typeof(IGradientControl), 
            propertyChanged: UpdateCanvas);

        public static readonly BindableProperty GradientRepeatProperty = BindableProperty.Create(
            nameof(IGradientControl.GradientRepeat), 
            typeof(BackgroundRepeat), 
            typeof(IGradientControl), 
            propertyChanged: UpdateCanvas);

        public static readonly BindableProperty MaskProperty = BindableProperty.Create(
            nameof(IGradientControl.Mask),
            typeof(GradientMask), 
            typeof(IGradientControl), 
            propertyChanged: OnGradientElementChanged);

        private static void OnGradientElementChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var visualElement = (IGradientVisualElement)bindable;

            if (oldValue != null && oldValue is GradientElement oldElem)
                oldElem.Parent = null;

            if (newValue != null && newValue is GradientElement newElem)
                newElem.Parent = visualElement;

            visualElement.InvalidateCanvas();
        }

        static void UpdateCanvas(BindableObject bindable, object oldValue, object newValue)
        {
            var visualElement = (IGradientVisualElement)bindable;
            visualElement.InvalidateCanvas();
        }
    }
}
