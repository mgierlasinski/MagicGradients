using MagicGradients.Renderers;
using SkiaSharp;
using Xamarin.Forms;

namespace MagicGradients.Masks
{
    public class EllipseMask : GradientMask
    {
        public static readonly BindableProperty SizeProperty = BindableProperty.Create(nameof(Size),
            typeof(Dimensions), typeof(TextMask), Dimensions.Prop(1, 1));

        public Dimensions Size
        {
            get => (Dimensions)GetValue(SizeProperty);
            set => SetValue(SizeProperty, value);
        }

        public override void Clip(RenderContext context)
        {
            if(!IsActive)
                return;

            using (new CanvasLock(context.Canvas))
            {
                var width = (int)Size.Width.GetPixels(context.RenderRect.Width);
                var height = (int)Size.Height.GetPixels(context.RenderRect.Height);

                var bounds = new SKRectI(0, 0, width, height);
                var ellipse = new SKRoundRect(bounds, (float)width / 2, (float)height / 2);

                context.Canvas.Translate(
                    (float)context.RenderRect.Width / 2 - bounds.MidX, 
                    (float)context.RenderRect.Height / 2 - bounds.MidY);
                
                context.Canvas.ClipRoundRect(ellipse, ClipMode.ToSkOperation(), true);
            }
        }
    }
}
