using System.Threading.Tasks;
using Xamarin.Essentials;

namespace Playground.Features.Share
{
    public class ShareService : IShareService
    {
        public Task ShareText(string title, string text)
        {
            return Xamarin.Essentials.Share.RequestAsync(new ShareTextRequest
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
}
