﻿using Microsoft.Maui.Graphics;
using SkiaSharp;

namespace MagicGradients.Graphics.Skia
{
    public static class SkiaExtensions
    {
        public static SKPoint ToSKPoint(this PointF point)
        {
            return new SKPoint(point.X, point.Y);
        }

        public static RectangleF ToRectF(this SKRectI rect)
        {
            return new RectangleF(rect.Left, rect.Top, rect.Width, rect.Height);
        }
    }
}