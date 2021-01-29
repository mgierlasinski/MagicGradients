﻿using SkiaSharp;

namespace MagicGradients.Masks
{
    public enum FillMode
    {
        Center,
        AspectFit,
        AspectFill,
        Fill
    }

    public enum ClipMode
    {
        Include,
        Exclude
    }

    public static class ClipModeExtensions
    {
        public static SKClipOperation ToSkOperation(this ClipMode mode)
        {
            return mode == ClipMode.Include ? SKClipOperation.Intersect : SKClipOperation.Difference;
        }
    }
}
