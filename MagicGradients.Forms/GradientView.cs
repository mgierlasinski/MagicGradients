using MagicGradients.Drawing;
using MagicGradients.Masks;
using Xamarin.Forms;

namespace MagicGradients.Forms
{
    [ContentProperty(nameof(GradientSource))]
    public class GradientView : GraphicsView, IGradientControl, IGradientVisualElement
    {
        static GradientView()
        {
            StyleSheetsConfig.RegisterStyles<GradientView>();
        }

        public static readonly BindableProperty GradientSourceProperty = GradientControl.GradientSourceProperty;
        public static readonly BindableProperty GradientSizeProperty = GradientControl.GradientSizeProperty;
        public static readonly BindableProperty GradientRepeatProperty = GradientControl.GradientRepeatProperty;
        public static readonly BindableProperty MaskProperty = GradientControl.MaskProperty;

        public IGradientSource GradientSource
        {
            get => (IGradientSource)GetValue(GradientSourceProperty);
            set => SetValue(GradientSourceProperty, value);
        }

        public Dimensions GradientSize
        {
            get => (Dimensions)GetValue(GradientSizeProperty);
            set => SetValue(GradientSizeProperty, value);
        }

        public BackgroundRepeat GradientRepeat
        {
            get => (BackgroundRepeat)GetValue(GradientRepeatProperty);
            set => SetValue(GradientRepeatProperty, value);
        }

        public IGradientMask Mask
        {
            get => (IGradientMask)GetValue(MaskProperty);
            set => SetValue(MaskProperty, value);
        }

        public GradientView()
        {
            Drawable = new GradientDrawable(this);
        }

        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();

            if (GradientSource is BindableObject bindable)
            {
                SetInheritedBindingContext(bindable, BindingContext);
            }

            if (Mask is BindableObject maskBindable)
            {
                SetInheritedBindingContext(maskBindable, BindingContext);
            }
        }

        public void InvalidateCanvas()
        {
            //InvalidateSurface();
        }
    }
}
