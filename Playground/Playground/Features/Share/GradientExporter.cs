using MagicGradients;
using System;
using System.Linq;
using System.Text;

namespace Playground.Features.Share
{
    public class GradientExporter : IGradientExporter
    {
        public string ExportCss(ExportData data)
        {
            var builder = new StringBuilder();

            builder.AppendLine($"background: {ExportGradients(data.GradientSource)};");

            if (!data.GradientSize.IsZero)
            {
                builder.AppendLine($"background-size: {ExportSize(data.GradientSize)};");
                builder.AppendLine($"background-repeat: {ExportRepeat(data.GradientRepeat)};");
            }

            return builder.ToString();
        }

        public string ExportRaw(ExportData data)
        {
            var builder = new StringBuilder();

            builder.Append($"{ExportGradients(data.GradientSource)};");

            if (!data.GradientSize.IsZero)
            {
                builder.Append($"{ExportSize(data.GradientSize)};");
                builder.Append($"{ExportRepeat(data.GradientRepeat)};");
            }

            return builder.ToString();
        }

        private string ExportGradients(IGradientSource source) => string.Join(",", source.GetGradients().Reverse().Select(GetGradient));

        private string GetGradient(Gradient gradient)
        {
            if (gradient is LinearGradient linear)
            {
                var type = linear.IsRepeating ? "repeating-linear-gradient" : "linear-gradient";
                var angle = $"{GradientMath.FromDegrees(linear.Angle)}deg";

                return $"{type}({angle}, {GetColors(linear)})";
            }

            if (gradient is RadialGradient radial)
            {
                var type = radial.IsRepeating ? "repeating-radial-gradient" : "radial-gradient";

                return $"{type}({GetShapeAndSize(radial)} {GetPosition(radial)}, {GetColors(radial)})";
            }

            return string.Empty;
        }

        private string GetColors(Gradient gradient)
        {
            return string.Join(", ", gradient.Stops.Select(x =>
            {
                var color = $"rgba({Math.Floor(x.Color.R * 255)},{Math.Floor(x.Color.G * 255)},{Math.Floor(x.Color.B * 255)},{x.Color.A})";
                return $"{color} {x.RenderOffset * 100}%";
            }));
        }

        private string GetOffset(Offset offset, double fallback)
        {
            if (offset.IsEmpty)
                return $"{fallback * 100}%";

            return offset.Type == OffsetType.Proportional
                ? $"{offset.Value * 100}%"
                : $"{offset.Value}px";
        }

        private string GetShapeAndSize(RadialGradient gradient)
        {
            if (gradient.RadiusX > -1 && gradient.RadiusY > -1)
            {
                var radiusX = FlagsHelper.IsSet(gradient.Flags, RadialGradientFlags.WidthProportional)
                    ? $"{gradient.RadiusX * 100}%"
                    : $"{gradient.RadiusX}px";

                var radiusY = FlagsHelper.IsSet(gradient.Flags, RadialGradientFlags.HeightProportional)
                    ? $"{gradient.RadiusY * 100}%"
                    : $"{gradient.RadiusY}px";

                return $"ellipse {radiusX} {radiusY}";
            }

            var shape = gradient.Shape.ToString().ToLower();
            var size = gradient.Size switch
            {
                RadialGradientSize.ClosestSide => "closest-side",
                RadialGradientSize.ClosestCorner => "closest-corner",
                RadialGradientSize.FarthestSide => "farthest-side",
                _ => "farthest-corner"
            };

            return $"{shape} {size}";
        }

        private string GetPosition(RadialGradient gradient)
        {
            var center = gradient.Center;

            var posX = FlagsHelper.IsSet(gradient.Flags, RadialGradientFlags.XProportional)
                ? $"{center.X * 100}%"
                : $"{center.X}px";

            var posY = FlagsHelper.IsSet(gradient.Flags, RadialGradientFlags.YProportional)
                ? $"{center.Y * 100}%"
                : $"{center.Y}px";

            return $"at {posX} {posY}";
        }

        private string ExportSize(Dimensions size) => $"{GetOffset(size.Width, 1)} {GetOffset(size.Height, 1)}";

        private string ExportRepeat(BackgroundRepeat repeat)
        {
            return repeat switch
            {
                BackgroundRepeat.Repeat => "repeat",
                BackgroundRepeat.RepeatX => "repeat-x",
                BackgroundRepeat.RepeatY => "repeat-y",
                _ => "no-repeat"
            };
        }
    }
}
