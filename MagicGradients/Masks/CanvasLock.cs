using SkiaSharp;
using System;

namespace MagicGradients.Masks
{
    public class CanvasLock : IDisposable
    {
        private readonly SKCanvas _canvas;
        private readonly SKMatrix _matrix;

        public CanvasLock(SKCanvas canvas)
        {
            _canvas = canvas;
            _matrix = canvas.TotalMatrix;
        }

        public void Dispose()
        {
            _canvas.SetMatrix(_matrix);
        }
    }
}
