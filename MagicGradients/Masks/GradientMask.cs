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

        public static readonly BindableProperty StretchProperty = BindableProperty.Create(nameof(Stretch),
            typeof(Stretch), typeof(GradientMask), Stretch.None);

        public static readonly BindableProperty IsActiveProperty = BindableProperty.Create(nameof(IsActive),
            typeof(bool), typeof(GradientMask), true);

        public ClipMode ClipMode
        {
            get => (ClipMode)GetValue(ClipModeProperty);
            set => SetValue(ClipModeProperty, value);
        }

        public Stretch Stretch
        {
            get => (Stretch)GetValue(StretchProperty);
            set => SetValue(StretchProperty, value);
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

            if (Stretch == Stretch.None)
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

                if (Stretch == Stretch.AspectFit)
                    context.Canvas.Scale(Math.Min(scaleX, scaleY));

                if (Stretch == Stretch.AspectFill)
                    context.Canvas.Scale(Math.Max(scaleX, scaleY));

                if (Stretch == Stretch.Fill)
                    context.Canvas.Scale(scaleX, scaleY);
            }

            context.Canvas.Translate(-bounds.MidX, -bounds.MidY);
        }
    }
}
