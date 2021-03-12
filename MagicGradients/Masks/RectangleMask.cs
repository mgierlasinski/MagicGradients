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
            var width = (int)Size.Width.GetPixels(context.CanvasRect.Width);
            var height = (int)Size.Height.GetPixels(context.CanvasRect.Height);

            var bounds = new SKRectI(0, 0, width, height);
            var roundRect = new SKRoundRect();

            roundRect.SetRectRadii(bounds, new[]
            {
                GetCornerPoint(Corners.TopLeft, bounds),
                GetCornerPoint(Corners.TopRight, bounds),
                GetCornerPoint(Corners.BottomRight, bounds),
                GetCornerPoint(Corners.BottomLeft, bounds)
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

        private SKPoint GetCornerPoint(Dimensions cornerSize, SKRectI bounds)
        {
            return new SKPoint(
                (int)cornerSize.Width.GetPixels(bounds.Width), 
                (int)cornerSize.Height.GetPixels(bounds.Height));
        }
    }
}
