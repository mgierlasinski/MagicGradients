namespace Playground.Features.Share
{
    public interface IGradientExporter
    {
        string ExportCss(ExportData data);
        string ExportRaw(ExportData data);
    }
}
