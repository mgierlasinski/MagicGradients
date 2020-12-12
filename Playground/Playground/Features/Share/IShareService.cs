using System.Threading.Tasks;

namespace Playground.Features.Share
{
    public interface IShareService
    {
        Task ShareText(string title, string text);
        Task CopyToClipboard(string text);
    }
}
