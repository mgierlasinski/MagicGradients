using System;
using SkiaSharp;

namespace MagicGradients.Renderers
{
    public class GradientRenderer
    {
        public void Render(RenderContext context, IGradientShader shader)
        {
            context.Paint.Shader = shader.Create(context);

            if (context.Size.Width.Value > 0 && context.Size.Height.Value > 0)
            {
                RenderTiled(context);
            }
            else
            {
                context.Canvas.DrawRect(context.Info.Rect, context.Paint);
            }
        }

        private void RenderTiled(RenderContext context)
        {
            var tileWidth = context.Size.Width.Type == OffsetType.Proportional
                ? (float)context.Size.Width.Value * context.Info.Width
                : (float)context.Size.Width.Value;

            var tileHeight = context.Size.Height.Type == OffsetType.Proportional
                ? (float)context.Size.Height.Value * context.Info.Height
                : (float)context.Size.Height.Value;

            var rows = Math.Ceiling(context.Info.Height / tileHeight);
            var cols = Math.Ceiling(context.Info.Width / tileWidth);
            var scaleX = tileWidth / context.Info.Width;
            var scaleY = tileHeight / context.Info.Height;

            for (var row = 0; row < rows; row++)
            {
                for (var col = 0; col < cols; col++)
                {
                    var point = new SKPoint(col * tileWidth, row * tileHeight);

                    context.Canvas.Save();
                    context.Canvas.Translate(point);
                    context.Canvas.Scale(scaleX, scaleY);
                    context.Canvas.DrawRect(context.Info.Rect, context.Paint);
                    context.Canvas.Restore();
                }
            }
        }
    }
}
