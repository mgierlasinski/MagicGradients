using MagicGradients.Drawing;
using SkiaSharp;
using System.Linq;

namespace MagicGradients.Forms.SkiaViews.Drawing
{
    public class LinearGradientPainter : GradientPainter
    {
        public SKShader CreateShader(ILinearGradient gradient, DrawContext context)
        {
            var rect = context.RenderRect;

            var line = new LinearGradientGeometry();
            line.CalculateOffsets(gradient, context.RenderRect.Width, context.RenderRect.Height);
            line.CalculateGeometry(gradient, rect.ToRectF());

            var renderStops = GetRenderStops(gradient);
            var colors = renderStops.Select(x => x.Color.ToSKColor()).ToArray();
            var colorPos = renderStops.Select(x => x.RenderOffset).ToArray();

            var startPoint = line.Start;
            var endPoint = line.End;

            if (gradient.IsRepeating)
            {
                var firstOffset = renderStops.FirstOrDefault()?.RenderOffset ?? 0;
                var lastOffset = renderStops.LastOrDefault()?.RenderOffset ?? 1;

                startPoint = line.GetColorPointAt(firstOffset);
                endPoint = line.GetColorPointAt(lastOffset);

                for (var i = 0; i < colorPos.Length; i++)
                {
                    colorPos[i] = line.ScaleWithBias(colorPos[i], firstOffset, lastOffset, 0, 1);
                }
            }

            var shader = SKShader.CreateLinearGradient(
                startPoint.ToSKPoint(),
                endPoint.ToSKPoint(),
                colors,
                colorPos,
                gradient.IsRepeating ? SKShaderTileMode.Repeat : SKShaderTileMode.Clamp);

            return shader;
        }
    }
}
