using System;
using SkiaSharp;

namespace MagicGradients.Renderers
{
    public class GradientRenderer
    {
        public void Render(RenderContext context, IGradientShader shader)
        {
            context.Paint.Shader = shader.Create(context);

            if (context.Size.Width > 1 && context.Size.Height > 1)
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
            var rows = Math.Ceiling(context.Info.Height / context.Size.Height);
            var cols = Math.Ceiling(context.Info.Width / context.Size.Width);
            var scaleX = context.Size.Width / context.Info.Width;
            var scaleY = context.Size.Height / context.Info.Height;

            for (var row = 0; row < rows; row++)
            {
                for (var col = 0; col < cols; col++)
                {
                    var point = new SKPoint(col * context.Size.Width, row * context.Size.Height);

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
