using MagicGradients.Renderers;
using SkiaSharp;
using System;
using Xamarin.Forms;

namespace MagicGradients.Masks
{
    public class GradientMask : GradientElement
    {
        public static readonly BindableProperty ClipModeProperty = BindableProperty.Create(nameof(ClipMode),
            typeof(ClipMode), typeof(GradientMask), ClipMode.Include);

        public static readonly BindableProperty FillModeProperty = BindableProperty.Create(nameof(FillMode),
            typeof(FillMode), typeof(GradientMask), FillMode.Center);

        public static readonly BindableProperty IsActiveProperty = BindableProperty.Create(nameof(IsActive),
            typeof(bool), typeof(GradientMask), true);

        public ClipMode ClipMode
        {
            get => (ClipMode)GetValue(ClipModeProperty);
            set => SetValue(ClipModeProperty, value);
        }

        public FillMode FillMode
        {
            get => (FillMode)GetValue(FillModeProperty);
            set => SetValue(FillModeProperty, value);
        }

        public bool IsActive
        {
            get => (bool)GetValue(IsActiveProperty);
            set => SetValue(IsActiveProperty, value);
        }

        public virtual void Clip(RenderContext context)
        {
        }

        protected void LayoutBounds(RenderContext context, SKRect bounds, bool keepAspectRatio)
        {
            context.Canvas.Translate((float)context.RenderRect.Width / 2, (float)context.RenderRect.Height / 2);

            if (FillMode == FillMode.Center)
            {
                var scaleX = (float)context.RenderRect.Width / context.CanvasRect.Width;
                var scaleY = (float)context.RenderRect.Height / context.CanvasRect.Height;

                if(keepAspectRatio)
                    context.Canvas.Scale(Math.Max(scaleX, scaleY));
                else
                    context.Canvas.Scale(scaleX, scaleY);
            }
            else
            {
                var scaleX = context.RenderRect.Width / bounds.Width;
                var scaleY = context.RenderRect.Height / bounds.Height;

                if (FillMode == FillMode.AspectFit)
                    context.Canvas.Scale(Math.Min(scaleX, scaleY));

                if (FillMode == FillMode.AspectFill)
                    context.Canvas.Scale(Math.Max(scaleX, scaleY));

                if (FillMode == FillMode.Fill)
                    context.Canvas.Scale(scaleX, scaleY);
            }

            context.Canvas.Translate(-bounds.MidX, -bounds.MidY);
        }
    }
}
