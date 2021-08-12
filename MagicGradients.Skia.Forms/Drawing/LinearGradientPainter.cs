using MagicGradients.Maui.Graphics.Drawing;
using SkiaSharp;
using SkiaSharp.Views.Forms;
using System.Linq;

namespace MagicGradients.Skia.Forms.Drawing
{
    public class LinearGradientPainter : GradientPainter
    {
        public SKShader CreateShader(LinearGradient gradient, DrawContext context)
        {
            var rect = context.RenderRect;

            var renderStops = GetRenderStops(gradient);
            var colors = renderStops.Select(x => x.Color.ToSKColor()).ToArray();
            var colorPos = renderStops.Select(x => x.RenderOffset).ToArray();

            var line = new LinearGradientGeometry(rect.ToRectF(), gradient.Angle);
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
