using MagicGradients.Renderers;
using SkiaSharp;
using Xamarin.Forms;

namespace MagicGradients.Masks
{
    public class RectangleMask : GradientMask
    {
        public static readonly BindableProperty SizeProperty = BindableProperty.Create(nameof(Size),
            typeof(Dimensions), typeof(RectangleMask), Dimensions.Prop(1, 1));

        public static readonly BindableProperty CornersProperty = BindableProperty.Create(nameof(Corners),
            typeof(Corners), typeof(RectangleMask));

        public Dimensions Size
        {
            get => (Dimensions)GetValue(SizeProperty);
            set => SetValue(SizeProperty, value);
        }

        public Corners Corners
        {
            get => (Corners)GetValue(CornersProperty);
            set => SetValue(CornersProperty, value);
        }

        public override void Clip(RenderContext context)
        {
            if (!IsActive)
                return;

            var roundRect = GetRoundRect(context);
            ClipRoundRect(context, roundRect);
        }

        private SKRoundRect GetRoundRect(RenderContext context)
        {
            var width = (int)Size.Width.GetDrawPixels(context.CanvasRect.Width, context.PixelScaling);
            var height = (int)Size.Height.GetDrawPixels(context.CanvasRect.Height, context.PixelScaling);

            var bounds = new SKRectI(0, 0, width, height);
            var roundRect = new SKRoundRect();

            roundRect.SetRectRadii(bounds, new[]
            {
                GetCornerPoint(Corners.TopLeft, bounds, context.PixelScaling),
                GetCornerPoint(Corners.TopRight, bounds, context.PixelScaling),
                GetCornerPoint(Corners.BottomRight, bounds, context.PixelScaling),
                GetCornerPoint(Corners.BottomLeft, bounds, context.PixelScaling)
            });

            return roundRect;
        }

        protected internal void ClipRoundRect(RenderContext context, SKRoundRect roundRect)
        {
            using (new CanvasLock(context.Canvas))
            {
                LayoutBounds(context, roundRect.Rect, false);
                context.Canvas.ClipRoundRect(roundRect, ClipMode.ToSkOperation(), true);
            }
        }

        private SKPoint GetCornerPoint(Dimensions cornerSize, SKRectI bounds, double pixelScaling)
        {
            return new SKPoint(
                (int)cornerSize.Width.GetDrawPixels(bounds.Width, pixelScaling), 
                (int)cornerSize.Height.GetDrawPixels(bounds.Height, pixelScaling));
        }
    }
}
