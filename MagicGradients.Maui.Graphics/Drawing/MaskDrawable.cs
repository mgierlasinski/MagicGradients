﻿using MagicGradients.Masks;
using Microsoft.Maui.Graphics;
using System;
using PathF = Microsoft.Maui.Graphics.PathF;

namespace MagicGradients.Maui.Graphics.Drawing
{
    public class MaskDrawable
    {
        public void Clip(DrawContext context, GradientMask mask)
        {
            if (mask is EllipseMask ellipseMask)
                ClipMask(context, ellipseMask);

            else if (mask is RectangleMask rectangleMask)
                ClipMask(context, rectangleMask);

            else if (mask is PathMask pathMask)
                ClipMask(context, pathMask);
        }

        private void ClipMask(DrawContext context, RectangleMask mask)
        {
            var bounds = GetSizeRect(context, mask.Size);

            var path = new PathF();
            path.AppendRoundedRectangle(bounds, 
                GetRadius(mask.Corners.TopLeft),
                GetRadius(mask.Corners.TopRight),
                GetRadius(mask.Corners.BottomLeft),
                GetRadius(mask.Corners.BottomRight));

            LayoutBounds(context, bounds, mask.Stretch, false);
            context.Canvas.ClipPath(path);

            float GetRadius(Dimensions cornerSize) => (float)cornerSize.Width.GetDrawPixels((int)bounds.Width, 1);
        }

        private void ClipMask(DrawContext context, EllipseMask mask)
        {
            var bounds = GetSizeRect(context, mask.Size);

            var path = new PathF();
            path.AppendEllipse(bounds);
            
            LayoutBounds(context, bounds, mask.Stretch, false);
            context.Canvas.ClipPath(path);
        }

        private void ClipMask(DrawContext context, PathMask mask)
        {
            
        }

        private RectangleF GetSizeRect(DrawContext context, Dimensions size)
        {
            var width = (float)size.Width.GetDrawPixels((int)context.CanvasRect.Width, 1);
            var height = (float)size.Height.GetDrawPixels((int)context.CanvasRect.Height, 1);

            return new RectangleF(0, 0, width, height);
        }
        
        private void LayoutBounds(DrawContext context, RectangleF bounds, Stretch stretch, bool keepAspectRatio)
        {
            context.Canvas.Translate((float)context.RenderRect.Width / 2, (float)context.RenderRect.Height / 2);

            if (stretch == Stretch.None)
            {
                var scaleX = (float)context.RenderRect.Width / context.CanvasRect.Width;
                var scaleY = (float)context.RenderRect.Height / context.CanvasRect.Height;

                if (keepAspectRatio)
                {
                    var scale = Math.Max(scaleX, scaleY);
                    context.Canvas.Scale(scale, scale);
                }
                else
                    context.Canvas.Scale(scaleX, scaleY);
            }
            else
            {
                var scaleX = context.RenderRect.Width / bounds.Width;
                var scaleY = context.RenderRect.Height / bounds.Height;

                if (stretch == Stretch.AspectFit)
                {
                    var scale = Math.Min(scaleX, scaleY);
                    context.Canvas.Scale(scale, scale);
                }

                if (stretch == Stretch.AspectFill)
                {
                    var scale = Math.Max(scaleX, scaleY);
                    context.Canvas.Scale(scale, scale);
                }

                if (stretch == Stretch.Fill)
                    context.Canvas.Scale(scaleX, scaleY);
            }

            context.Canvas.Translate(-bounds.Center.X, -bounds.Center.Y);
        }
    }
}
