using MagicGradients;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace Playground.Features.Editor.Services
{
    public interface IShareService
    {
        Task ShareText(string title, string text);
        Task CopyToClipboard(string text);
    }

    public class ShareService : IShareService
    {
        public Task ShareText(string title, string text)
        {
            return Share.RequestAsync(new ShareTextRequest
            {
                Title = title,
                Text = text
            });
        }

        public Task CopyToClipboard(string text)
        {
            return Clipboard.SetTextAsync(text);
        }
    }

    public class ExportData
    {
        public IGradientSource GradientSource { get; set; }
        public Dimensions GradientSize { get; set; }
        public BackgroundRepeat GradientRepeat { get; set; }
    }

    public interface IGradientExporter
    {
        string ExportCss(ExportData data);
        string ExportRaw(ExportData data);
    }

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

        private string ExportGradients(IGradientSource source)
        {
            return string.Join(",", source.GetGradients().Select(GetGradient));
        }

        private string GetGradient(Gradient gradient)
        {
            if (gradient is LinearGradient linear)
            {
                var type = linear.IsRepeating ? "repeating-linear-gradient" : "linear-gradient";
                return $"{type}({GetAngle(linear)}, {GetColors(linear)})";
            }

            if (gradient is RadialGradient radial)
            {
                var type = radial.IsRepeating ? "repeating-radial-gradient" : "radial-gradient";
                return $"{type}({GetColors(radial)})";
            }

            return string.Empty;
        }

        private string GetColors(Gradient gradient)
        {
            return string.Join(",", gradient.Stops.Select(x =>
            {
                var color = $"rgba({Math.Floor(x.Color.R * 255)},{Math.Floor(x.Color.G * 255)},{Math.Floor(x.Color.B * 255)},{x.Color.A})";
                return $"{color} {x.RenderOffset * 100}%";
            }));
        }

        private string GetAngle(LinearGradient gradient)
        {
            return $"{GradientMath.FromDegrees(gradient.Angle)}deg";
        }

        private string ExportSize(Dimensions size)
        {
            return $"{size.Width.Value}px {size.Height.Value}px";
        }

        private string ExportRepeat(BackgroundRepeat repeat)
        {
            return "...";
        }
    }
}
